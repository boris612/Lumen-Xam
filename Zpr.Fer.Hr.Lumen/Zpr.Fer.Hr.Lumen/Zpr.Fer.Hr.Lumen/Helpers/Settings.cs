// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Linq;

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
        private const string DifficultyOptionKey = "difficulty_option_key";
        private const string LanguageKey = "language_key";
        private const string CorrectGuessesKey = "correct_guesses_key";
        private const string FalseGuessesKey = "false_guesses_key";

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

        public static int DifficultyOption
        {
            get
            {
                return AppSettings.GetValueOrDefault(DifficultyOptionKey, -1);
            }
            set
            {
                if (value >= -1 && value <= 2)
                    AppSettings.AddOrUpdateValue(DifficultyOptionKey, value);
            }
        }

        public static string Language
        {
            get
            {
                return AppSettings.GetValueOrDefault(LanguageKey, Constants.CroatianLanguge.Code);
            }
            set
            {
                if (Constants.Languages.Any(x => x.Code == value))
                    AppSettings.AddOrUpdateValue(LanguageKey, value);
            }
        }

        public static int CorrectGuesses
        {
            get
            {
                return AppSettings.GetValueOrDefault(CorrectGuessesKey, default(int));
            }
            set
            {
                if (value >= 0)
                    AppSettings.AddOrUpdateValue(CorrectGuessesKey, value);
            }
        }

        public static int FalseGuesses
        {
            get
            {
                return AppSettings.GetValueOrDefault(FalseGuessesKey, default(int));
            }
            set
            {
                if (value >= 0)
                    AppSettings.AddOrUpdateValue(FalseGuessesKey, value);
            }
        }
    }
}