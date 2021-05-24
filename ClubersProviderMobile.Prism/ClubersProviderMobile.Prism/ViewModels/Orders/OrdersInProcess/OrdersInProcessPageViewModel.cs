using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Orders;
using ClubersProviderMobile.Prism.Views.Orders.OrdersInProcess;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Xml.Schema;
using Xamarin.Forms;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class OrdersInProcessPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private List<Orders> _myorders;
        private ObservableCollection<Orders> _orders;
        private string _search;
        private Orders _selectedOrder;
        private string _clockIcon = IconFont.ClockTimeThreeOutline;

        private DelegateCommand<Orders> _selectedOrderCommand;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _searchCommand;

        public OrdersInProcessPageViewModel(INavigationService navigationService,
                                            IPageDialogService dialogService,
                                            IApiService apiService) : base(navigationService)
        {
            Title = "En proceso";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            LoadProcessOrdersAsync();
        }
        public DelegateCommand<Orders> OrderSelectedCommand => _selectedOrderCommand ?? (_selectedOrderCommand = new DelegateCommand<Orders>(OrderDetails));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowOrders));

        public string ClockIcon
        {
            get => _clockIcon;
            set => SetProperty(ref _clockIcon, value);
        }
        public ObservableCollection<Orders> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
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
        public Orders SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadProcessOrdersAsync();
        }
        private void RefreshData()
        {
            LoadProcessOrdersAsync();
            IsRefreshing = false;
        }
        private async void LoadProcessOrdersAsync()
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
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }

            _myorders = response.Result.Where(x => x.ConsumptionTypeId == (int)ConsumptionTypes.Delivery && x.TrackingStatusId == (int)TrackingStatus.InProcess).ToList();
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
            await _navigationService.NavigateAsync(nameof(OrdersInProcessDetailPage), parameters);
            SelectedOrder = null;
        }
    }
}
