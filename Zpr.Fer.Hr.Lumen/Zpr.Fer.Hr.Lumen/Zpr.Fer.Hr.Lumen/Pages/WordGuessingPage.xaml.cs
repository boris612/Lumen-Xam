﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zpr.Fer.Hr.Lumen.Models;

namespace Zpr.Fer.Hr.Lumen.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGuessingPage : ContentPage
    {
        public static Label StatusLabel { get; set; }
        public static Button ConfirmButton { get; set; }
        public static Button RetryButton { get; set; }
        public static Word Word { get; set; }

        private static Dictionary<BoxView, bool> _boxViewEmpty;
        private static Dictionary<Image, BoxView> _boxViewForImage;
        private static List<BoxView> _wordBoxViews;
        private static List<Image> _previewLetters;
        private static Image _image;
        public WordGuessingPage()
        {
            InitializeComponent();

            _boxViewEmpty = new Dictionary<BoxView, bool>();
            _boxViewForImage = new Dictionary<Image, BoxView>();
            _wordBoxViews = new List<BoxView>();
            _previewLetters = new List<Image>();
            Word = GameWordUtils.GetRandomWord();

            var letters = GameWordUtils.GetLettersForWord(Word);
            var columnLength = letters.Count + 2;

            #region GridInit
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.4, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.125, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.05, GridUnitType.Star) });
            for (var i = 0; i < columnLength; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.9 / columnLength, GridUnitType.Star) });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.05, GridUnitType.Star) });
            #endregion

            var mainImage = new Image
            {
                Source = Word.ImagePath
            };
            grid.Children.Add(mainImage, 1, grid.ColumnDefinitions.Count, 0, 1);
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => TapGestureRecognizer_Tapped(s, e);

            #region GuessingBoardInit

            for (var i = 0; i < letters.Count; i++)
            {
                var image = new Image()
                {
                    Source = letters[i].ImagePath
                };
                _previewLetters.Add(image);
                grid.Children.Add(image, i + 2, 1);
                var boxView = new BoxView()
                {
                    Color = Color.LightYellow,
                    Opacity = 0
                };

                boxView.GestureRecognizers.Add(tapGestureRecognizer);
                grid.Children.Add(boxView, i + 2, 1);
                _boxViewEmpty.Add(boxView, true);
                _wordBoxViews.Add(boxView);
            }
            #endregion

            #region OfferedLettersBoardInit
            var rnd = GameWordUtils.Random;
            var randomLetters = GameWordUtils.GetRandomLetters(columnLength * 2);
            var gridChildrenIndexOffset = 1 + 2 * letters.Count;

            //Fill grid with box views
            for (var i = 0; i < 2; i++)
                for (var j = 0; j < columnLength; j++)
                {
                    var boxView = new BoxView
                    {
                        Color = Color.LightYellow,
                        Opacity = 0
                    };
                    boxView.GestureRecognizers.Add(tapGestureRecognizer);
                    _boxViewEmpty.Add(boxView, true);
                    grid.Children.Add(boxView, j + 1, i + 3);
                }

            //Fill random box views with word characters
            for (var i = 0; i < letters.Count; i++)
            {
                var row = rnd.Next(2);
                var column = rnd.Next(columnLength);
                var index = gridChildrenIndexOffset + row * columnLength + column;
                var boxView = (BoxView)grid.Children[index];
                if (_boxViewEmpty[boxView])
                {
                    var image = new Image
                    {
                        Source = letters[i].ImagePath,
                        Opacity = 0
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
                            Source = randomLetters[row * columnLength + column].ImagePath, 
                            Opacity = 0
                        };
                        image.GestureRecognizers.Add(tapGestureRecognizer);

                        _boxViewEmpty[boxView] = false;
                        _boxViewForImage.Add(image, boxView);
                        grid.Children.Add(image, column + 1, row + 3);
                    }
                }
            }
            #endregion

            ConfirmButton = new Button
            {
                BackgroundColor = Color.LightYellow,
                Text = "Potvrdi",
                Opacity = 0
            };
            ConfirmButton.Clicked += ConfirmButton_Clicked;
            RetryButton = new Button
            {
                BackgroundColor = Color.DarkRed,
                Text = "Ponovi",
                Opacity = 0
            };
            RetryButton.Clicked += RetryButton_Clicked;

            grid.Children.Add(ConfirmButton, 3, 2);
            grid.Children.Add(RetryButton, 4, 2);

            StatusLabel = new Label
            {
                IsVisible = false,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Opacity = 0
            };
            grid.Children.Add(StatusLabel, 2, 2);
            Content = grid;
            StartPreview();
        }

        private void RetryButton_Clicked1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            StatusLabel.IsVisible = false;
            StatusLabel.Text = string.Empty;
            string word = "";
            int letterCounter = 0;
            foreach (var box in _wordBoxViews)
            {
                var image = _boxViewForImage.Where(x => x.Value == box).Select(x => x.Key).FirstOrDefault();
                if (image == null)
                {
                    letterCounter++;
                    continue;
                }
                var letter = ((FileImageSource)image.Source).File.Replace(".png", "").ToUpper();
                switch (letter)
                {
                    case "CC":
                        word += "Ć"; break;
                    case "CH":
                        word += "Č"; break;
                    case "ZZ":
                        word += "Ž"; break;
                    case "DD":
                        word += "Đ"; break;
                    case "DZ":
                        word += "DŽ"; break;
                    case "SS":
                        word += "Š"; break;
                    default:
                        word += letter; break;
                }

                if (GameWordUtils.CheckLetter(letterCounter, letter))
                {
                    box.Color = Color.Green;
                }
                else
                {
                    box.Color = Color.Red;
                }
                letterCounter++;
            }
            if (word == Word.Name)
            {
                StatusLabel.TextColor = Color.Green;
                StatusLabel.Text = "TOČNO";
            }
            else
            {
                StatusLabel.TextColor = Color.Red;
                StatusLabel.Text = "KRIVO";
            }
            StatusLabel.IsVisible = true;
        }

        private async void RetryButton_Clicked(object sender, EventArgs e)
        {
            var lastPage = Navigation.NavigationStack.First();
            await Navigation.PushAsync(new WordGuessingPage(), true);
            Navigation.RemovePage(lastPage);
        }
        private async void StartPreview()
        {
            var previewFadeOut = new Animation();
            var gameFadeIn = new Animation();
            #region AddFadeoutAnimations
            foreach (var letter in _previewLetters)
            {
                previewFadeOut.Add(0, 1, new Animation(
                    a => letter.Opacity = a, 1, 0, Easing.SinOut));
            }
            #endregion
            #region AddFadeInAnimations
            foreach (var box in _boxViewEmpty)
            {
                gameFadeIn.Add(0, 1, new Animation(
                    a => box.Key.Opacity = a, 0, 1, Easing.SinIn));
            }
            foreach (var image in _boxViewForImage)
            {
                gameFadeIn.Add(0, 1, new Animation(
                    a => image.Key.Opacity = a, 0, 1, Easing.SinIn));
            }
            gameFadeIn.Add(0, 1, new Animation(
                    a => ConfirmButton.Opacity = a, 0, 1, Easing.SinIn));
            gameFadeIn.Add(0, 1, new Animation(
                    a => RetryButton.Opacity = a, 0, 1, Easing.SinIn));
            gameFadeIn.Add(0, 1, new Animation(
                    a => StatusLabel.Opacity = a, 0, 1, Easing.SinIn)); 
            #endregion

            await Task.Delay(5000);

            previewFadeOut.Commit(
                owner: StatusLabel,
                name: "FadeOut",
                length: 500);

            gameFadeIn.Commit(
                owner: StatusLabel,
                name: "FadeIn",
                length: 500);


        }
    }
}
