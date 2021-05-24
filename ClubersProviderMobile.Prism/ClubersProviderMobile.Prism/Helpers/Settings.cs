using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace ClubersProviderMobile.Prism.Helpers
{
    public static class Settings
    {
        private const string _user = "user";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _isRemembered = "IsRemembered";
        private const string _isAvailable = "IsAvailable";
        private const string _deactivateReceivingOrderDate = "deactivateReceivingOrderDate";
        private const string _activateReceivingOrderDate = "activateReceivingOrderDate";

        private static readonly bool _boolDefault = false;
        private static readonly string _stringDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;
        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
        public static bool IsAvailable
        {
            get => AppSettings.GetValueOrDefault(_isAvailable, !_boolDefault);
            set => AppSettings.AddOrUpdateValue(_isAvailable, value);
        }
        public static string DeactivateReceivingOrderDate
        {
            get => AppSettings.GetValueOrDefault(_deactivateReceivingOrderDate, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_deactivateReceivingOrderDate, value);
        }
        public static string ActivateReceivingOrderDate
        {
            get => AppSettings.GetValueOrDefault(_activateReceivingOrderDate, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_activateReceivingOrderDate, value);
        }
        public static string User
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }
        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }
        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
    }
}
