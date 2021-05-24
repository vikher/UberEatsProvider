using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Orders;
using ClubersProviderMobile.Prism.Views.Orders.ReadyOrders;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ReadyOrdersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private List<Orders> _myorders;
        private ObservableCollection<Orders> _orders;
        private Orders _selectedOrder;
        private string _search;

        private DelegateCommand<Orders> _selectedOrderCommand;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _searchCommand;
        public ReadyOrdersPageViewModel(INavigationService navigationService,
                                       IPageDialogService dialogService,
                                       IApiService apiService) : base(navigationService)
        {
            Title = "Listos";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            LoadReadyOrdersAsync();
        }
        public DelegateCommand<Orders> OrderSelectedCommand => _selectedOrderCommand ?? (_selectedOrderCommand = new DelegateCommand<Orders>(OrderDetails));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public ObservableCollection<Orders> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        public Orders SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowOrders();
            }
        }
        private void SetProperty(ref object orders, ObservableCollection<Orders> value)
        {
            throw new NotImplementedException();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadReadyOrdersAsync();
        }
        private void RefreshData()
        {
            LoadReadyOrdersAsync();
            IsRefreshing = false;
        }
        private async void LoadReadyOrdersAsync()
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

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.ResultMessages[0],
                    "Aceptar");
                return;
            }

            _myorders = response.Result.Where(x => x.ConsumptionTypeId == (int)ConsumptionTypes.Delivery && x.TrackingStatusId == (int)TrackingStatus.Ready).ToList();
            ShowOrders();

            IsRunning = false;
            IsEnabled = true;
        }
        private void ShowOrders()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Orders = new ObservableCollection<Orders>(_myorders);
            }
            else
            {
                Orders = new ObservableCollection<Orders>(_myorders.Where(o => o.Id == int.Parse(Search)));
            }
        }
        private async void OrderDetails(Orders obj)
        {
            if (SelectedOrder == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.order, SelectedOrder }
            };
            await _navigationService.NavigateAsync(nameof(ReadyOrdersDetailsPage), parameters);
            SelectedOrder = null;
        }
    }
}
