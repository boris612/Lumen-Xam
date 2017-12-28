using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zpr.Fer.Hr.Lumen.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            if (Helpers.Settings.GreenField == "True") {
                greenFieldToogle.IsToggled = true;
            } else
            {
                greenFieldToogle.IsToggled = false;
            }

            if (Helpers.Settings.MoreLetters == "True")
            {
                moreLettersToogle.IsToggled = true;
            }
            else
            {
                moreLettersToogle.IsToggled = false;
            }
        }

        private void onGreenFieldToggle(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            Lumen.Helpers.Settings.GreenField = isToggled.ToString();
        }

        private void onMoreLettersToggle(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            Lumen.Helpers.Settings.MoreLetters = isToggled.ToString();
        }
    }
}
