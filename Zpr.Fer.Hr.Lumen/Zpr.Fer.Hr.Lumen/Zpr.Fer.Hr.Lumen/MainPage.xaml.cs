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
        public MainPage()
        {
            InitializeComponent();
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            //Slovo.TranslateTo(e.TotalX, e.TotalY);
            Slovo.Layout(new Rectangle(Slovo.X + e.TotalX, Slovo.Y + e.TotalY, Slovo.Width, Slovo.Height));
        }
    }
}
