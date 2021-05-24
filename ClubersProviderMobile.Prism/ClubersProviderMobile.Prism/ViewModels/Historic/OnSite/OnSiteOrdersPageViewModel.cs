using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;
using ClubersProviderMobile.Prism.Views;
using Prism.Commands;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ClubersProviderMobile.Prism.Helpers;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class OnSiteOrdersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Orders _selectedOrder;
        private ObservableCollection<Orders> _orders;
        private DelegateCommand<Orders> _selectedOrderCommand;
        private DelegateCommand _refreshViewCommand;

        public OnSiteOrdersPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "En sitio";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            LoadOrdersAsync();
        }
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand<Orders> OrderSelectedCommand => _selectedOrderCommand ?? (_selectedOrderCommand = new DelegateCommand<Orders>(OrderDetails));
        public Orders SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        public ObservableCollection<Orders> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadOrdersAsync();
        }
        private void RefreshData()
        {
            LoadOrdersAsync();
            IsRefreshing = false;
        }
        private async void LoadOrdersAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            string url = App.Current.Resources["UrlAPI"].ToString();
            OrdersResponse response = await _apiService.GetAllOrdersByEstablishmentIdAsync(url, Constants.servicePrefix, Constants.GetAllOrdersByEstablishmentIdAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }

            Orders = new ObservableCollection<Orders>(response.Result.Where(x => x.ConsumptionTypeId == (int)ConsumptionTypes.OnSite && x.TrackingStatusId == (int)TrackingStatus.Delivered).ToList());
        }

        private async void OrderDetails(Orders obj)
        {
            if (SelectedOrder == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.order, SelectedOrder }
            };
            await _navigationService.NavigateAsync(nameof(OnSiteOrderDetailPage), parameters);
            SelectedOrder = null;
        }
    }
}
