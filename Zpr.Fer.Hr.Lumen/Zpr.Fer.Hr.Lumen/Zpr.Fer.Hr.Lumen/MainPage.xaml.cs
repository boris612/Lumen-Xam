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
        public static List<BoxView> Boxes { get; set; }
        public MainPage()
        {
            
            InitializeComponent();

            Boxes = new List<BoxView>
            {
                Box1, Box2, Box3, Box4, Box5
            };
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    Slovo.TranslateTo(e.TotalX, e.TotalY);
                    Slovo.Layout(new Rectangle(Slovo.X + e.TotalX, Slovo.Y + e.TotalY, Slovo.Width, Slovo.Height));
                    foreach (var box in Boxes)
                    {
                        if (Slovo.X + Slovo.Width/2 > box.X && Slovo.X + Slovo.Width/2 < box.X + box.Width
                            && Slovo.Y + Slovo.Height/2 > box.Y && Slovo.Y + Slovo.Height/2 < box.Y + box.Height)
                        {
                            box.Color = Color.Yellow;
                        }
                        else
                        {
                            box.Color = Color.Blue;
                        }
                    }
                    break;
            }
        }
    }
}
