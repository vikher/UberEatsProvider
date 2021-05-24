using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ScanQrPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
                      
        private List<Customers> _customers;
        private Customers _customer;

        private DelegateCommand _selectMenuCommand;
        private DelegateCommand _goQrScanCommand;        

        public ScanQrPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService): base(navigationService)
        {
            Title = "Lector de Código QR";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand GoIdCustomerPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        public DelegateCommand GoQrCodeReaderPageCommand => _goQrScanCommand ?? (_goQrScanCommand = new DelegateCommand(GoQrCodeReaderAsync));
        public List<Customers> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }
        public Customers Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsRunning = true;
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("Id"))
            {
                var Id = parameters.GetValue<string>("Id");
                ScanId(Id);
            }
            IsRunning = false;
        }
        private async void SelectMenuAsync()
        {
            IsRunning = true;
            await _navigationService.NavigateAsync(nameof(IdCustomerPage));
            IsRunning = false;
        }
        private async void GoQrCodeReaderAsync()
        {
            IsRunning = true;
            await _navigationService.NavigateAsync(nameof(QrCodeReaderPage));
            IsRunning = false;
        }
        private async void ScanId(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.idValid, Constants.cancelButton);
                return;
            }
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            Response response = await _apiService.GetCustomerByIdAsync(url, Constants.servicePrefix, Constants.GetCustomerByIdAsyncController, int.Parse(Id), user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }

            Customer = (Customers)response.Result;

            var parameters = new NavigationParameters
                            {
                                { Constants.customer, Customer }
                            };
            await _navigationService.NavigateAsync(nameof(CommandByCustomer), parameters);

        }
    }
}
