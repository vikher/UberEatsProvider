using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Interfaces;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
using ClubersProviderMobile.Prism.Views.Login;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isEnabled;
        private bool _isRemember;

        private DelegateCommand _loginCommand;
        private DelegateCommand _forgotPasswordCommand;
        public LoginPageViewModel(INavigationService navigationService, IApiService apiService, IRegexHelper regexHelper, IPageDialogService dialogService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regexHelper = regexHelper;
            Title = "Inicio";
            IsRemember = true;
        }
        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await _dialogService.DisplayAlertAsync("Error", "Debe ingresar un correo.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await _dialogService.DisplayAlertAsync("Error", "Debe ingresar una contraseña.", "Aceptar");
                return;
            }

            if (Password.Length < 6)
            {
                await _dialogService.DisplayAlertAsync("Error", "La contraseña debe contener al menos 6 carácteres", "Aceptar");
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Compruebe la conexión a Internet", "Aceptar");
                return;
            }

            TokenRequest request = new TokenRequest
            {
                password = Password,
                email = Email
            };

            Response response = await _apiService.GetTokenAsync(Constants.urlBase, "api/Account", "/token", request);

            if (response.ResultCode != ResultCode.Success)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Email o contraseña incorrectos", "Aceptar");
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            Response response2 = await _apiService.GetUserByEmailAsync(Constants.urlBase, Constants.servicePrefix , Constants.GetUserByEmailAsyncController, Email, Constants.tokenType, token.Result.token);

            UserResponse userResponse = (UserResponse)response2.Result;

            if (userResponse.Result.RoleId != (int)RoleSystem.Provider)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "El usuario no es un socio proveedor", "Aceptar");
                return;
            }

            Settings.User = JsonConvert.SerializeObject(userResponse);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/ProviderMasterDetailPage/NavigationPage/OrdersTabbedPage?selectedTab=NewOrdersPage");
            Password = string.Empty;
            Settings.IsRemembered = IsRemember;
        }

        private async void ForgotPasswordAsync()
        {
            await _navigationService.NavigateAsync(nameof(RecoverPasswordPage));
        }


        public bool IsRemember
        {
            get => _isRemember;
            set => SetProperty(ref _isRemember, value);
        }

    }
}
