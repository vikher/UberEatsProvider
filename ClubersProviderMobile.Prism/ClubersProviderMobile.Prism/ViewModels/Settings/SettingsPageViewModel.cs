using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Xamarin.Essentials;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private Establishment _establishment;
        private bool _isEnabled;
        private bool _isVisible;
        private UserResponse _user;
        private EstablishmentResponse _establishmentResponse;
        private string _imageUrl;

        public SettingsPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            Title = "Configuración";
            LoadUser();
        }
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public Establishment Establishment
        {
            get => _establishment;
            set => SetProperty(ref _establishment, value);
        }

        public EstablishmentResponse EstablishmentResponse
        {
            get => _establishmentResponse;
            set => SetProperty(ref _establishmentResponse, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadEstablishmentAsync();
        }

        private async void LoadEstablishmentAsync()
        {

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Compruebe la conexión a Internet", "Aceptar");
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetEstablishmentByIdAsync(url, Constants.servicePrefix, Constants.GetEstablishmentByIdAsyncController, User.Result.Establishment.Id, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.ResultMessages[0],
                    "Aceptar");
                return;
            }

            EstablishmentResponse = (EstablishmentResponse)response.Result;
            CheckVisibility();
        }
        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        private void CheckVisibility()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Monday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Tuesday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Wednesday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Thursday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Friday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Saturday;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                IsVisible = EstablishmentResponse.Result.EstablishmentHours.Sunday;
            }

        }

    }
}
