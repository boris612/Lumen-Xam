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
        private static Image _image;
        public MainPage()
        {
            InitializeComponent();
            _boxViewEmpty = new Dictionary<BoxView, bool>
            {
                {Box1, false },
                {Box2, true },
                {Box3, true },
                {Box4, false },
                {Box5, true },
                {Box6, true },
                {Box7, false },
                {Box8, false },
                {Box9, true },
                {Box10, false },
                {Box11, true },
                {Box12, true },
                {Box13, false },
                {Box14, false },
                {Box15, false }
            };
            _boxViewForImage = new Dictionary<Image, BoxView>
            {
                {LetterA, Box1 },
                {LetterD, Box4 },
                {LetterB, Box7 },
                {LetterC, Box8 },
                {LetterE, Box10 },
                {LetterF, Box15 },
                {LetterG, Box13 },
                {LetterH, Box14 }
            };
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is BoxView boxView)
            {
                boxView = (BoxView)sender;
                if(_image != null && _boxViewEmpty[boxView])
                {
					_image.TranslateTo(boxView.X - _image.X, boxView.Y - _image.Y);
                    _boxViewEmpty[_boxViewForImage[_image]] = true;
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(_image, boxView);
                    _boxViewEmpty[boxView] = false;
                    _image = null;
                }
            }
            else _image = (Image)sender;
        }
    }
}
