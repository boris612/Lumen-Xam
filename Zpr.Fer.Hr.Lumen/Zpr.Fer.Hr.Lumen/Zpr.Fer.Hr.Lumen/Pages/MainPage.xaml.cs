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

        private async void StartWordGuessingGameButton_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPage();
            await Navigation.PushAsync(loadingPage);
            var wordGuessingPage = new WordGuessingPage();
            await Navigation.PushAsync(wordGuessingPage);
            Navigation.RemovePage(loadingPage);
            wordGuessingPage.StartPreview();
        }

        private async void StartCoinGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoinGamePage());
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
