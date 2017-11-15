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
                {Box1, true},
                {Box2, true},
                {Box3, true},
                {Box4, true},
                {Box5, true},
                {Box6, false},
            };
            _boxViewForImage = new Dictionary<Image, BoxView>
            {
                {Slovo, Box6}
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
