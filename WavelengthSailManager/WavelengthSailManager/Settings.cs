using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Essentials;

namespace WavelengthSailManager
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        private const string FirstRunKey = "first_run_key";
        private static readonly Boolean FirstRunDefault = true;


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static Boolean FirstRun
        {
            get
            {
                return AppSettings.GetValueOrDefault(FirstRunKey, FirstRunDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FirstRunKey, value);
            }
        }

    }
}
