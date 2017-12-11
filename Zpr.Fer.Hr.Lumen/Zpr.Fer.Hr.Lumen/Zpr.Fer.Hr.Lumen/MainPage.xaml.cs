using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            var word = App.Database.GetWord();
            _boxViewEmpty = new Dictionary<BoxView, bool>
            {
                {Box1, true },
                {Box2, true },
                {Box3, true },
                {Box4, true },
                {Box5, true },
                {Box6, true },
                {Box7, true },
                {Box8, false },
                {Box9, false },
                {Box10, false },
                {Box11, false },
                {Box12, false },
                {Box13, false },
                {Box14, false },
                {Box15, false }
            };
            _boxViewForImage = new Dictionary<Image, BoxView>
            {
                {LetterA, Box8 },
                {LetterD, Box11 },
                {LetterB, Box9 },
                {LetterC, Box10 },
                {LetterE, Box12 },
                {LetterF, Box13 },
                {LetterG, Box14 },
                {LetterH, Box15 }
            };

            _wordBoxViews = new List<BoxView>
            {
                Box1, Box2, Box3, Box4, Box5, Box6, Box7
            };
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
            WordLabel.IsVisible = false;
            WordLabel.Text = string.Empty;
            foreach (var box in _wordBoxViews)
            {
                var image = _boxViewForImage.Where(x => x.Value == box).Select(x => x.Key).FirstOrDefault();
                if (image == null) continue;
                var letter = ((FileImageSource)image.Source).File.Replace(".png", "").ToUpper();
                switch (letter)
                {
                    case "CC":
                        WordLabel.Text += "Ć"; break;
                    case "CH":
                        WordLabel.Text += "Č"; break;
                    case "ZZ":
                        WordLabel.Text += "Ž"; break;
                    case "DD":
                        WordLabel.Text += "Đ"; break;
                    case "DZ":
                        WordLabel.Text += "DŽ"; break;
                    case "SS":
                        WordLabel.Text += "Š"; break;
                    default:
                        WordLabel.Text += letter; break;
                }
            }
            WordLabel.IsVisible = true;
        }
    }
}
