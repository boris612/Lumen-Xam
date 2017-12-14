using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zpr.Fer.Hr.Lumen.Models;

namespace Zpr.Fer.Hr.Lumen
{
    public partial class MainPage : ContentPage
    {
        private static Dictionary<BoxView, bool> _boxViewEmpty;
        private static Dictionary<Image, BoxView> _boxViewForImage;
        private static List<BoxView> _wordBoxViews;
        private static Image _image;
        public MainPage()
        {
            InitializeComponent();
            _boxViewEmpty = new Dictionary<BoxView, bool>();
            _boxViewForImage = new Dictionary<Image, BoxView>();
            _wordBoxViews = new List<BoxView>();
            var word = new Word
            {
                Name = "GOL",
                ImagePath = "gol.jpg"
            };
            var letters = word.Name.ToCharArray();
            #region GridInit
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.4, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.05, GridUnitType.Star) });
            for (var i = 0; i < letters.Length + 2; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.9 / (letters.Length + 2), GridUnitType.Star) });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.05, GridUnitType.Star) });
            #endregion

            var mainImage = new Image
            {
                Source = word.ImagePath
            };
            grid.Children.Add(mainImage, 1, grid.ColumnDefinitions.Count, 0, 1);
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => TapGestureRecognizer_Tapped(s, e);

            #region GuessingBoardInit

            for (var i = 0; i < letters.Length; i++)
            {
                var boxView = new BoxView();
                boxView.Color = Color.AliceBlue;
                boxView.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(boxView, i + 2, 1);
                _boxViewEmpty.Add(boxView, true);
                _wordBoxViews.Add(boxView);
            }
            #endregion

            #region OfferedLettersBoardInit
            var rnd = new Random();
            var columnLength = letters.Length + 2;
            var gridChildrenIndexOffset = 1 + letters.Length;
            var allLetters = "ABCDEFGHIJKLMNOOPRSTUVZ";

            //Fill grid with box views
            for (var i = 0; i < 2; i++)
                for (var j = 0; j < letters.Length + 2; j++)
                {
                    var boxView = new BoxView
                    {
                        Color = Color.AliceBlue
                    };
                    boxView.GestureRecognizers.Add(tapGestureRecognizer);
                    _boxViewEmpty.Add(boxView, true);
                    grid.Children.Add(boxView, j + 1, i + 3);
                }

            //Fill random box views with word characters
            for (var i = 0; i < letters.Length; i++)
            {
                var row = rnd.Next(2);
                var column = rnd.Next(letters.Length + 2);
                var index = gridChildrenIndexOffset + row * columnLength + column;
                var boxView = (BoxView)grid.Children[index];
                if (_boxViewEmpty[boxView])
                {
                    var image = new Image
                    {
                        Source = letters[i] + ".png"
                    };
                    
                    image.GestureRecognizers.Add(tapGestureRecognizer);
                    _boxViewEmpty[boxView] = false;
                    _boxViewForImage.Add(image, boxView);
                    grid.Children.Add(image, column + 1, row + 3);
                }
                else i--;
            }

            //Fill remaining box views with random characters
            for (var row = 0; row <= 1; row++)
            {
                for (var column = 0; column < columnLength; column++)
                {
                    var index = gridChildrenIndexOffset + row * columnLength + column;
                    var boxView = (BoxView)grid.Children[index];
                    if (_boxViewEmpty[boxView])
                    {
                        var image = new Image
                        {
                            Source = allLetters[rnd.Next(allLetters.Length)] + ".png"
                        };
                        image.GestureRecognizers.Add(tapGestureRecognizer);

                        _boxViewEmpty[boxView] = false;
                        _boxViewForImage.Add(image, boxView);
                        grid.Children.Add(image, column + 1, row + 3);
                    }
                }
            }
            #endregion
            Content = grid;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BoxView)
            {
                var boxView = (BoxView)sender;
                if (_image != null && _boxViewEmpty[boxView])
                {
                    _image.TranslateTo(boxView.X - _image.X, boxView.Y - _image.Y);
                    _image.Opacity = 1;
                    _boxViewEmpty[_boxViewForImage[_image]] = true;
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(_image, boxView);
                    _boxViewEmpty[boxView] = false;
                    _image = null;
                }
            }
            else
            {
                var image = (Image)sender;
                // if another letter was tapped before
                if (_image != null)
                {
                    if (_image == image) return;
                    _image.Opacity = 1;
                    var box1 = _boxViewForImage[image];
                    var box2 = _boxViewForImage[_image];
                    _boxViewForImage.Remove(image);
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(image, box2);
                    _boxViewForImage.Add(_image, box1);
                    image.TranslateTo(box2.X - image.X, box2.Y - image.Y);
                    _image.TranslateTo(box1.X - _image.X, box1.Y - _image.Y);
                    _image = null;
                }
                else
                {
                    _image = (Image)sender;
                    _image.HorizontalOptions = LayoutOptions.Fill;
                    _image.Opacity = .6;
                }
            }
        }

        private void ComfirmButton_Clicked(object sender, EventArgs e)
        {
            //WordLabel.IsVisible = false;
            //WordLabel.Text = string.Empty;
            foreach (var box in _wordBoxViews)
            {
                var image = _boxViewForImage.Where(x => x.Value == box).Select(x => x.Key).FirstOrDefault();
                if (image == null) continue;
                var letter = ((FileImageSource)image.Source).File.Replace(".png", "").ToUpper();
                //switch (letter)
                //{
                //    case "CC":
                //        WordLabel.Text += "Ć"; break;
                //    case "CH":
                //        WordLabel.Text += "Č"; break;
                //    case "ZZ":
                //        WordLabel.Text += "Ž"; break;
                //    case "DD":
                //        WordLabel.Text += "Đ"; break;
                //    case "DZ":
                //        WordLabel.Text += "DŽ"; break;
                //    case "SS":
                //        WordLabel.Text += "Š"; break;
                //    default:
                //        WordLabel.Text += letter; break;
                //}
            }
            //WordLabel.IsVisible = true;
        }
    }
}
