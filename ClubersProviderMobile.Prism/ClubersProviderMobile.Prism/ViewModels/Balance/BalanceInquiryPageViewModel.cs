using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;


namespace ClubersProviderMobile.Prism.ViewModels
{
    public class BalanceInquiryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private List<Order> _orders;
        private bool _isRunning;

        private readonly IApiService _apiService;
        public BalanceInquiryPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Saldo";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            LoadOrdersAsync();

        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public List<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadOrdersAsync();
        }

        private async void LoadOrdersAsync()
        {

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            //Response response = await _apiService.GetAllOrdersAsync<Order>(Constants.urlBase, Constants.servicePrefix, Constants.controller, Constants.tokenType, Constants.accessToken);
            //Orders = (List<Order>)response.Result;

            //Orders = Orders.Where(x => x.StatusName == StatusName.cancelled).ToList();
        }
    }
}
