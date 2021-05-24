using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
using ClubersProviderMobile.Prism.Views.Orders;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CanceledOrdersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private ObservableCollection<Orders> _orders;
        private Orders _selectedOrder;

        private DelegateCommand<Orders> _selectedOrderCommand;
        public CanceledOrdersPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Cancelados";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            LoadOrdersAsync();
        }
        public DelegateCommand<Orders> OrderSelectedCommand => _selectedOrderCommand ?? (_selectedOrderCommand = new DelegateCommand<Orders>(OrderDetails));
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
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadOrdersAsync();
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
            OrdersResponse response = await _apiService.GetAllOrdersByEstablishmentIdAsync(url, Constants.servicePrefix, Constants.GetAllOrdersByEstablishmentIdAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token); ;

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }


            Orders = new ObservableCollection<Orders>(response.Result.Where(x => x.TrackingStatusId == (int)TrackingStatus.Canceled).ToList());
        }
        private async void OrderDetails(Orders obj)
        {
            if (SelectedOrder == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.order, SelectedOrder }
            };
            if (SelectedOrder.ConsumptionTypeId == (int)ConsumptionTypes.Delivery)
            {
                await _navigationService.NavigateAsync(nameof(CancelledDeliveryDetailPage), parameters);
            }
            else if(SelectedOrder.ConsumptionTypeId == (int)ConsumptionTypes.OnSite)
            {
                await _navigationService.NavigateAsync(nameof(CancelledCollaboratorDetailPage), parameters);
            }
            SelectedOrder = null;
        }
    }
}
