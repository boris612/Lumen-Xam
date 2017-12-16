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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void StartWordGuessingGameButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WordPreviewPage());
        }

        private void StartCoinGameButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CoinGamePage());
        }

        private void SettingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }
    }
}
