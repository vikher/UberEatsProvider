using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
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
    public class ReviewCommandPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private List<Orders> _myorders;
        private ObservableCollection<Orders> _orders;
        private string _search;
        private Orders _selectedOrder;
        private Customer _Customer;
        private EstablishmentStaff _collaborator;

        private DelegateCommand<Orders> _orderDetailCommand;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _searchCommand;
        public ReviewCommandPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "En revisión";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            LoadOrdersAsync();
        }
        public DelegateCommand<Orders> CommandDetailPageSelectedCommand => _orderDetailCommand ?? (_orderDetailCommand = new DelegateCommand<Orders>(OrderDetails));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowOrders));
        #region Properties
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
        public Customer Customer
        {
            get => _Customer;
            set => SetProperty(ref _Customer, value);
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
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
        #endregion Properties
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

           
            _myorders = response.Result.Where(x => x.ConsumptionTypeId == 3 && x.TrackingStatusId == 4).ToList();
            ShowOrders();
        }
        private void ShowOrders()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Orders = new ObservableCollection<Orders>(_myorders);
            }
            else
            {
                try
                {
                    Orders = new ObservableCollection<Orders>(_myorders.Where(o => o.Id == int.Parse(Search)));
                }
                catch
                {
                  
                    Orders = new ObservableCollection<Orders>(_myorders.Where(o => o.Customer.User.FullName.ToUpper().Contains(Search.ToUpper())));
                }
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
            await _navigationService.NavigateAsync(nameof(ReviewCommandDetailPage), parameters);
            SelectedOrder = null;
            
        }
    }
}
