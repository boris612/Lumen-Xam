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
        //public static List<BoxView> Boxes { get; set; }
        //private static double _x;
        //private static double _y;
        private static Dictionary<BoxView, bool> _boxViewEmpty;
        private static Dictionary<Image, BoxView> _boxViewForImage;
        private static Image _image;
        public MainPage()
        {
            
            InitializeComponent();
            //Boxes = new List<BoxView>
            //{
            //    Box1, Box2, Box3, Box4, Box5, Box6
            //};
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
                    _image.Layout(new Rectangle(boxView.X, boxView.Y, _image.Width, _image.Height));
                    _boxViewEmpty[_boxViewForImage[_image]] = true;
                    _boxViewForImage.Remove(_image);
                    _boxViewForImage.Add(_image, boxView);
                    _boxViewEmpty[boxView] = false;
                    _image = null;
                }
            }
            else _image = (Image)sender;
        }

        //private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        //{
        //    var letter = ((Image)sender);
        //    switch (e.StatusType)
        //    {
        //        case GestureStatus.Started:
        //            _x = letter.X;
        //            _y = letter.Y;
        //            break;
        //        case GestureStatus.Running:
        //            letter.TranslateTo(e.TotalX, e.TotalY);
        //            letter.Layout(new Rectangle(letter.X + e.TotalX, letter.Y + e.TotalY, letter.Width, letter.Height));
        //            foreach (var box in Boxes)
        //            {
        //                if (Slovo.X + Slovo.Width/2 > box.X && Slovo.X + Slovo.Width/2 < box.X + box.Width
        //                    && Slovo.Y + Slovo.Height/2 > box.Y && Slovo.Y + Slovo.Height/2 < box.Y + box.Height)
        //                {
        //                    box.Color = Color.Blue;
        //                }
        //                else
        //                {
        //                    box.Color = Color.Yellow;
        //                }
        //            }
        //            break;
        //        default:
        //            foreach (var box in Boxes)
        //            {
        //                if (Slovo.X + Slovo.Width / 2 > box.X && Slovo.X + Slovo.Width / 2 < box.X + box.Width
        //                    && Slovo.Y + Slovo.Height / 2 > box.Y && Slovo.Y + Slovo.Height / 2 < box.Y + box.Height)
        //                {
        //                    box.Color = Color.Blue;
        //                    letter.TranslateTo(box.X - letter.X, box.Y - letter.Y);
        //                    letter.Layout(new Rectangle(box.X, box.Y, letter.Width, letter.Height));
        //                    return;
        //                }
        //            }
        //            letter.TranslateTo(_x - letter.X, _y - letter.Y);
        //            letter.Layout(new Rectangle(_x, _y, letter.Width, letter.Height));
        //            break;
        //    }
        //}
    }
}
