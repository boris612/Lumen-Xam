//using Android.Widget;
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
    public partial class SettingsPage : ContentPage
    {
        private IList<Difficulty> Difficulties { get; set; }

        public SettingsPage()
        {
            Difficulties = App.Database.GetAllDifficulties();
            InitializeComponent();
            foreach(var dif in Difficulties)
            {
                Picker.Items.Add(dif.Name);
            }
            var difficultyLevel = Helpers.Settings.DifficultyOption;
            var difficulty = Difficulties.Where(x => x.Level == difficultyLevel).Single();
            //Picker.SelectedItem = difficulty.Name;
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = (string)picker.SelectedItem;
            var difficulty = Difficulties.Where(x => x.Name == selectedItem).Single();
            Helpers.Settings.DifficultyOption = difficulty.Level;
        }
    }
}
