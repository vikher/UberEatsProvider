using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class IdCustomerPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private List<Customers> _customers;
        private Customers _customer;

        private DelegateCommand _selectMenuCommand;
        public IdCustomerPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "ID";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public string Id { get; set; }
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
        public DelegateCommand GoOpenCommandPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(LoadCustomerAsync));
        private async void SelectMenuAsync()
        {
            IsRunning = true;
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
        }

        private async void LoadCustomerAsync()
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
            Response response = await _apiService.GetCustomerByINEAsync(url, Constants.servicePrefix, Constants.GetCustomerByINEAsyncController, Id, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

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
