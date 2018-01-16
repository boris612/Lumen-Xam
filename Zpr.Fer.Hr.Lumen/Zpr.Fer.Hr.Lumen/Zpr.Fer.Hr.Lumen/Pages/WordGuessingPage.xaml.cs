using Plugin.SimpleAudioPlayer;
using System;
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
    public partial class WordGuessingPage : ContentPage, IDisposable
    {
        public Label CoinLabel { get; set; }
        public Image CoinImage { get; set; }
        public Image Tick { get; set; }
        public Image Cross { get; set; }
        //public static Button ConfirmButton { get; set; }
        public Button RetryButton { get; set; }
        //public static Button HintButton { get; set; }
        public Word Word { get; set; }

        private Dictionary<BoxView, bool> _boxViewEmpty;
        private Dictionary<Image, BoxView> _boxViewForImage;
        private List<BoxView> _wordBoxViews;
        private List<Image> _previewLetters;
        private List<Letter> _letters;
        private Image _image;
        private bool _disposed = false;
        private bool _hintRunning = false;

        private static Grid grid;
        public WordGuessingPage()
        {
            InitializeComponent();

            _boxViewEmpty = new Dictionary<BoxView, bool>();
            _boxViewForImage = new Dictionary<Image, BoxView>();
            _wordBoxViews = new List<BoxView>();
            _previewLetters = new List<Image>();
            Word = GameWordUtils.GetRandomWord();

            _letters = GameWordUtils.GetLettersForWord(Word);
            var columnLength = _letters.Count + 2;

            #region GridInit
            grid = new Grid();
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

            for (var i = 0; i < _letters.Count; i++)
            {
                var image = new Image()
                {
                    Source = _letters[i].ImagePath
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
            var gridChildrenIndexOffset = 1 + 2 * _letters.Count;

            if (Helpers.Settings.MoreLetters == "True")
            {
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
            }
            else
            {
                if (columnLength > _letters.Count)
                {
                    for (var j = 0; j < _letters.Count; j++)
                    {
                        var boxView = new BoxView
                        {
                            Color = Color.LightYellow,
                            Opacity = 0
                        };
                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                        _boxViewEmpty.Add(boxView, true);
                        grid.Children.Add(boxView, j + 1, 3);
                    }
                }
                else
                {
                    for (var j = 0; j < columnLength; j++)
                    {
                        var boxView = new BoxView
                        {
                            Color = Color.LightYellow,
                            Opacity = 0
                        };
                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                        _boxViewEmpty.Add(boxView, true);
                        grid.Children.Add(boxView, j + 1, 3);
                    }
                    for (var j = 0; j < _letters.Count - columnLength; j++)
                    {
                        var boxView = new BoxView
                        {
                            Color = Color.LightYellow,
                            Opacity = 0
                        };
                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                        _boxViewEmpty.Add(boxView, true);
                        grid.Children.Add(boxView, j + 1, 4);
                    }
                }
            }

            //Fill random box views with word characters
            if (Helpers.Settings.MoreLetters == "True")
            {
                for (var i = 0; i < _letters.Count; i++)
                {
                    var row = rnd.Next(2);
                    var column = rnd.Next(columnLength);
                    var index = gridChildrenIndexOffset + row * columnLength + column;
                    var boxView = (BoxView)grid.Children[index];
                    if (_boxViewEmpty[boxView])
                    {
                        var image = new Image
                        {
                            Source = _letters[i].ImagePath,
                            Opacity = 0
                        };

                        image.GestureRecognizers.Add(tapGestureRecognizer);
                        _boxViewEmpty[boxView] = false;
                        _boxViewForImage.Add(image, boxView);
                        grid.Children.Add(image, column + 1, row + 3);
                    }
                    else i--;
                }
            }
            else
            {
                if (columnLength > _letters.Count)
                {
                    for (var i = 0; i < _letters.Count; i++)
                    {
                        var column = rnd.Next(_letters.Count);
                        var index = gridChildrenIndexOffset + column;
                        var boxView = (BoxView)grid.Children[index];
                        if (_boxViewEmpty[boxView])
                        {
                            var image = new Image
                            {
                                Source = _letters[i].ImagePath,
                                Opacity = 0
                            };

                            image.GestureRecognizers.Add(tapGestureRecognizer);
                            _boxViewEmpty[boxView] = false;
                            _boxViewForImage.Add(image, boxView);
                            grid.Children.Add(image, column + 1, 3);
                        }
                        else i--;
                    }
                }
                else
                {
                    for (var i = 0; i < _letters.Count; i++)
                    {
                        var row = rnd.Next(2);
                        int column;
                        int index;
                        if (row == 0)
                        {
                            column = rnd.Next(columnLength);
                            index = gridChildrenIndexOffset + column;
                        }
                        else
                        {
                            column = rnd.Next(_letters.Count - columnLength);
                            index = gridChildrenIndexOffset + row * columnLength + column;
                        }
                        var boxView = (BoxView)grid.Children[index];
                        if (_boxViewEmpty[boxView])
                        {
                            var image = new Image
                            {
                                Source = _letters[i].ImagePath,
                                Opacity = 0
                            };

                            image.GestureRecognizers.Add(tapGestureRecognizer);
                            _boxViewEmpty[boxView] = false;
                            _boxViewForImage.Add(image, boxView);
                            grid.Children.Add(image, column + 1, row + 3);
                        }
                        else i--;
                    }
                }
            }

            if (Helpers.Settings.MoreLetters == "True")
            {
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
            }
            #endregion

            Tick = new Image
            {
                Source = "tick.png",
                IsVisible = false
            };
            Cross = new Image
            {
                Source = "cross.png",
                IsVisible = false
            };
            //ConfirmButton = new Button
            //{
            //    BackgroundColor = Color.LightYellow,
            //    Text = "Potvrdi",
            //    Opacity = 0
            //};
            //ConfirmButton.Clicked += ConfirmButton_Clicked;
            RetryButton = new Button
            {
                BackgroundColor = Color.DarkRed,
                Text = "Ponovi",
                Opacity = 0
            };
            RetryButton.Clicked += RetryButton_Clicked;
            CoinImage = new Image
            {
                Source = "coin.png", 
                Opacity = 0, 
                Margin = new Thickness(10)
            };
            var hintTapGestureRecognizer = new TapGestureRecognizer();
            hintTapGestureRecognizer.Tapped += HintButton_Clicked;
            CoinImage.GestureRecognizers.Add(hintTapGestureRecognizer);
            //HintButton = new Button
            //{
            //    BackgroundColor = Color.LightBlue,
            //    Text = "Hint",
            //    Opacity = 0
            //};
            //HintButton.Clicked += HintButton_Clicked;

            grid.Children.Add(Tick, grid.ColumnDefinitions.Count - 2, 1);
            grid.Children.Add(Cross, grid.ColumnDefinitions.Count - 2, 1);
            grid.Children.Add(RetryButton, 3, 2);
            grid.Children.Add(CoinImage, grid.ColumnDefinitions.Count - 2, 0);
            //CoinImage.Layout(new Rectangle(grid.Width - 10, 10, 10, 10));

            CoinLabel = new Label
            {
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Opacity = 0,
                Text = Helpers.Settings.Coin
            };
            CoinLabel.GestureRecognizers.Add(hintTapGestureRecognizer);
            grid.Children.Add(CoinLabel, grid.ColumnDefinitions.Count - 2, 0);
            CoinLabel.Layout(new Rectangle(grid.Width - 10, 10, 5, 5));
            Content = grid;

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BoxView boxView)
            {
                if (_image != null && _boxViewEmpty[boxView])
                {
                    _image.TranslateTo(boxView.X - _image.X, boxView.Y - _image.Y);
                    var oldBoxView = _boxViewForImage[_image];
                    _boxViewEmpty[oldBoxView] = true;
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(_image, boxView);
                    _boxViewEmpty[boxView] = false;
                    oldBoxView.Color = Color.LightYellow;
                    if (Helpers.Settings.GreenField == "True")
                    {
                        UpdateBoxViewColor(_image, boxView, oldBoxView);
                    }
                    _image = null;
                }
                //HintButton.Text = "Hint";
            }
            else
            {
                var image = (Image)sender;
                // if another letter was tapped before
                if (_image != null)
                {
                    if (_image == image)
                    {
                        var box = _boxViewForImage[_image];
                        box.Color = Color.LightYellow;
                        _image = null;
                        return;
                    }
                    var box1 = _boxViewForImage[image];
                    var box2 = _boxViewForImage[_image];
                    _boxViewForImage.Remove(image);
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(image, box2);
                    _boxViewForImage.Add(_image, box1);
                    image.TranslateTo(box2.X - image.X, box2.Y - image.Y);
                    _image.TranslateTo(box1.X - _image.X, box1.Y - _image.Y);
                    box2.Color = Color.LightYellow;
                    if (Helpers.Settings.GreenField == "True")
                    {
                        UpdateBoxViewColor(image, box2);
                        UpdateBoxViewColor(_image, box1);
                    }

                    _image = null;
                }
                else
                {
                    _image = (Image)sender;
                    _image.HorizontalOptions = LayoutOptions.Fill;
                    var box = _boxViewForImage[_image];
                    box.Color = Color.LightBlue;
                }
                //HintButton.Text = "Hint";
            }

            //while (_hintRunning) ;
            if (_wordBoxViews.All(x => !_boxViewEmpty[x]))
            {
                var word = "";
                for (var i = 0; i < _wordBoxViews.Count; i++)
                {
                    var box = _wordBoxViews[i];
                    var image = _boxViewForImage.Where(x => box == x.Value).Select(x => x.Key).SingleOrDefault();
                    if (image != null)
                    {
                        var imageSource = image.Source.ToString().Replace("File: ", "");
                        var letter = _letters.Where(x => x.ImagePath == imageSource).Select(x => x.Name).FirstOrDefault();
                        word += letter;
                    }
                }
                if (word == Word.Name)
                {
                    foreach(var boxImage in _boxViewForImage)
                    {
                        boxImage.Key.IsEnabled = false;
                    }
                    foreach(var boxempty in _boxViewEmpty)
                    {
                        boxempty.Key.IsEnabled = false;
                    }
                    CoinImage.IsEnabled = false;
                    CoinLabel.IsEnabled = false;
                    Cross.IsVisible = false;
                    if (Helpers.Settings.GreenField != "True")
                    {
                        foreach (var box in _wordBoxViews)
                            box.Color = Color.Green;
                    }
                    Tick.IsVisible = true;
                    //add coin
                    var coin = Convert.ToInt32(Helpers.Settings.Coin);
                    coin += Word.Difficulty + 2;
                    CoinLabel.Text = coin.ToString();
                    Helpers.Settings.Coin = coin.ToString();
                    Helpers.Settings.CorrectGuesses++;
                    //ConfirmButton.IsVisible = false;
                    //RetryButton.IsVisible = false;
                    //HintButton.IsVisible = false;

                    await Task.Delay(2000);
                    RetryButton_Clicked(sender, e);
                }
                else
                {
                    Tick.IsVisible = false;
                    Helpers.Settings.FalseGuesses++;
                    if (Helpers.Settings.GreenField != "True")
                    {
                        foreach (var box in _wordBoxViews)
                            box.Color = Color.Red;
                    }
                    Cross.IsVisible = true;
                }
            }
            else if (_wordBoxViews.Any(x => x.Color != Color.LightYellow))
            {
                Tick.IsVisible = false;
                Cross.IsVisible = false;
                foreach (var box in _wordBoxViews)
                {
                    if (_boxViewEmpty[box])
                        box.Color = Color.LightYellow;
                    else
                    {
                        var image = _boxViewForImage.Where(x => x.Value == box).Select(x => x.Key).Single();
                        if (Helpers.Settings.GreenField == "True")
                            UpdateBoxViewColor(image, box);
                        else
                            box.Color = Color.LightYellow;
                    }
                }
            }
            if (_image != null)
                _boxViewForImage[_image].Color = Color.LightBlue;
        }

        private void UpdateBoxViewColor(Image image, BoxView boxView, BoxView oldBoxView = null)
        {
            var boxViewIndex = _wordBoxViews.IndexOf(boxView);
            if (boxViewIndex != -1)
            {
                var imageSource = image.Source.ToString().Replace("File: ", "");
                var imageLetter = _letters.Where(x => x.ImagePath == imageSource).Select(x => x.Name).FirstOrDefault();
                var realLetter = GameWordUtils.GetLetterAtIndex(Word.Name, boxViewIndex);
                if (string.IsNullOrEmpty(realLetter))
                {
                    boxView.Color = Color.Red;
                }
                else if (imageLetter == realLetter)
                {
                    boxView.Color = Color.Green;
                }
                else
                {
                    boxView.Color = Color.Red;
                }
            }
            if (oldBoxView != null && oldBoxView.Color != Color.LightYellow)
                oldBoxView.Color = Color.LightYellow;
        }
        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            var word = "";
            for (var i = 0; i < _wordBoxViews.Count; i++)
            {
                var box = _wordBoxViews[i];
                var image = _boxViewForImage.Where(x => box == x.Value).Select(x => x.Key).SingleOrDefault();
                if(image != null)
                {
                    var imageSource = image.Source.ToString().Replace("File: ", "");
                    var letter = _letters.Where(x => x.ImagePath == imageSource).Select(x => x.Name).FirstOrDefault();
                    word += letter;
                }
            }
            if (word == Word.Name)
            {
                if(Helpers.Settings.GreenField != "True")
                {
                    foreach (var box in _wordBoxViews)
                        box.Color = Color.Green;
                }

                //add coin
                var coin = Convert.ToInt32(Helpers.Settings.Coin);
                coin += Word.Difficulty + 2;
                CoinLabel.Text = coin.ToString();
                Helpers.Settings.Coin = coin.ToString();
                Helpers.Settings.CorrectGuesses++;
                //ConfirmButton.IsVisible = false;
                //RetryButton.IsVisible = false;
                //HintButton.IsVisible = false;

                await Task.Delay(2000);
                RetryButton_Clicked(sender, e);
            }
            else
            {
                Helpers.Settings.FalseGuesses++;
                //if(Helpers.Settings.GreenField != "True")
                //{
                //    foreach (var box in _wordBoxViews)
                //        box.Color = Color.Green;
                //}
            }
            //StatusLabel.IsVisible = true;
        }

        private void RetryButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var loadingPage = new LoadingPage();
                var lastPage = Navigation.NavigationStack.Last();                
                Navigation.InsertPageBefore(loadingPage, lastPage);
                Navigation.PopAsync(true);
                this.Dispose();
                _disposed = true;
                var newPage = new WordGuessingPage();
                Navigation.InsertPageBefore(newPage, loadingPage);
                Navigation.PopAsync(true);
                newPage.StartPreview();
            }
            finally
            {
                this.Dispose();
            }
            

        }

        private async void HintButton_Clicked(object sender, EventArgs e)
        {
            CoinImage.IsEnabled = false;
            CoinLabel.IsEnabled = false;
            //HintButton.IsEnabled = false;
            var isSucessfull = false;
            var coin = Convert.ToInt32(Helpers.Settings.Coin);
            #region HintLogic
            if (coin == 0) return;
            for(var i = 0; i < _wordBoxViews.Count; i++)
            {
                var correctLetter = GameWordUtils.GetLetterAtIndex(Word.Name, i);
                if (_boxViewEmpty[_wordBoxViews[i]])
                {
                    var imageBoxes = _boxViewForImage.Where(x => !_wordBoxViews.Contains(x.Value)).ToList();
                    
                    foreach(var imageBox in imageBoxes)
                    {
                        var imageSource = imageBox.Key.Source.ToString().Replace("File: ", "");
                        var letter = _letters.Where(x => x.ImagePath == imageSource).FirstOrDefault();
                        if (letter.Name == correctLetter)
                        {
                            _hintRunning = true;
                            for (var j = 0; j < 3; j++)
                            {
                                _wordBoxViews[i].Color = Color.LightBlue;
                                imageBox.Value.Color = Color.LightBlue;
                                await Task.Delay(600);
                                _wordBoxViews[i].Color = Color.LightYellow;
                                imageBox.Value.Color = Color.LightYellow;
                                await Task.Delay(500);
                            }
                            isSucessfull = true;
                            _hintRunning = false;
                            break;
                        }
                    }
                    if (isSucessfull) break;
                }
                else
                {
                    var image = _boxViewForImage.Where(x => x.Value == _wordBoxViews[i]).Select(x => x.Key).SingleOrDefault();
                    var wordBoxLetter = _letters.Where(x => x.ImagePath == image.Source.ToString().Replace("File: ", "")).FirstOrDefault();
                    if(wordBoxLetter == null || wordBoxLetter.Name != correctLetter)
                    {
                        var imageBoxes = _boxViewForImage.Where(x => !_wordBoxViews.Contains(x.Value)).ToList();
                        foreach (var imageBox in imageBoxes)
                        {
                            var imageSource = imageBox.Key.Source.ToString().Replace("File: ", "");
                            var letter = _letters.Where(x => x.ImagePath == imageSource).FirstOrDefault();
                            if (letter.Name == correctLetter)
                            {
                                for (var j = 0; j < 4; j++)
                                {
                                    _wordBoxViews[i].Color = Color.LightBlue;
                                    imageBox.Value.Color = Color.LightBlue;
                                    await Task.Delay(600);
                                    _wordBoxViews[i].Color = Color.LightYellow;
                                    imageBox.Value.Color = Color.LightYellow;
                                    await Task.Delay(600);
                                }
                                isSucessfull = true;
                                break;
                            }
                        }
                        if (isSucessfull) break;
                    }
                }
            }
            #endregion
            if (isSucessfull)
            {
                coin -= 1;
                CoinLabel.Text = coin.ToString();
                Helpers.Settings.Coin = coin.ToString();
            }
            foreach(var box in _wordBoxViews)
                if(Helpers.Settings.GreenField == "True")
                {
                    var image = _boxViewForImage.Where(x => x.Value == box).Select(x => x.Key).SingleOrDefault();
                    if (image != null)
                        UpdateBoxViewColor(image, box);
                }
            //HintButton.IsEnabled = true;
            CoinImage.IsEnabled = true;
            CoinLabel.IsEnabled = true;
        }

        public async void StartPreview()
        {
            await Task.Delay(1000);
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
            //gameFadeIn.Add(0, 1, new Animation(
            //        a => Tick.Opacity = a, 0, 1, Easing.SinIn));
            gameFadeIn.Add(0, 1, new Animation(
                    a => RetryButton.Opacity = a, 0, 1, Easing.SinIn));
            //gameFadeIn.Add(0, 1, new Animation(
            //        a => HintButton.Opacity = a, 0, 1, Easing.SinIn));
            gameFadeIn.Add(0, 1, new Animation(
                    a => CoinLabel.Opacity = a, 0, 1, Easing.SinIn));
            gameFadeIn.Add(0, 1, new Animation(
                    a => CoinImage.Opacity = a, 0, 1, Easing.SinIn));
            #endregion

            //await Task.Delay(5000);
            var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            foreach (var letter in _letters)
            {
                player.Load(letter.SoundPath);
                player.Play();
                await Task.Delay((int)player.Duration + 1000);
            }

            previewFadeOut.Commit(
                owner: CoinLabel,
                name: "FadeOut",
                length: 500);

            gameFadeIn.Commit(
                owner: CoinLabel,
                name: "FadeIn",
                length: 500);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                grid = null;
                CoinImage = null;
                CoinLabel = null;
                Tick = null;
                Cross = null;
                RetryButton = null;
                Word = null;
                _boxViewEmpty.Clear();
                _boxViewEmpty = null;
                _boxViewForImage.Clear();
                _boxViewForImage = null;
                _wordBoxViews.Clear();
                _wordBoxViews = null;
                _previewLetters.Clear();
                _previewLetters = null;
                _letters.Clear();
                _letters = null;
                _image = null;
                this.Content = null;
                GC.Collect(); 
            }
            
        }
    }
}
