// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Zpr.Fer.Hr.Lumen.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings {
            get {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string GreenFieldKey = "green_field_key";
        private const string MoreLettersKey = "more_letters_key";
        private const string CoinKey = "coin_key";

        #endregion


        public static string GreenField {
            get {
                return AppSettings.GetValueOrDefault(GreenFieldKey, false.ToString());
            }
            set {
                AppSettings.AddOrUpdateValue(GreenFieldKey, value);
            }
        }

        public static string MoreLetters {
            get {
                return AppSettings.GetValueOrDefault(MoreLettersKey, true.ToString());
            }
            set {
                AppSettings.AddOrUpdateValue(MoreLettersKey, value);
            }
        }

        public static string Coin {
            get {
                return AppSettings.GetValueOrDefault(CoinKey, 0.ToString());
            }
            set {
                AppSettings.AddOrUpdateValue(CoinKey, value);
            }
        }
    }
}