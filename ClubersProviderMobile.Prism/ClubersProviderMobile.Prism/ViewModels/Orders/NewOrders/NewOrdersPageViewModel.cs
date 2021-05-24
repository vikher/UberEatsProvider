using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Views.Orders;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Helpers;
using Newtonsoft.Json;
using DryIoc;
using System.Collections.ObjectModel;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class NewOrdersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private bool _isReceivingOrders;
        private ObservableCollection<Orders> _newOrder;
        private Orders _selectedOrder;
        private EstablishmentResponse _establishmentResponse;
        private DelegateCommand<Orders> _selectedOrderCommand;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _pauseThirtyMinutesCommand;
        private DelegateCommand _pauseTodayCommand;

        public NewOrdersPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Nuevos";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            
            LoadEstablishmentAsync();
            LoadNewOrdersAsync();

        }
        public DelegateCommand<Orders> NewOrderSelectedCommand => _selectedOrderCommand ?? (_selectedOrderCommand = new DelegateCommand<Orders>(OrderDetails));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand PauseThirtyMinutesCommand => _pauseThirtyMinutesCommand ?? (_pauseThirtyMinutesCommand = new DelegateCommand(ScheduleThirtyMinutes));
        public DelegateCommand PauseTodayCommand => _pauseTodayCommand ?? (_pauseTodayCommand = new DelegateCommand(ScheduleToday));

        public ObservableCollection<Orders> NewOrders
        {
            get => _newOrder;
            set => SetProperty(ref _newOrder, value);
        }
        public EstablishmentResponse EstablishmentResponse
        {
            get => _establishmentResponse;
            set => SetProperty(ref _establishmentResponse, value);
        }
        public bool IsReceivingOrders
        {
            get => _isReceivingOrders;
            set 
            {
                SetProperty(ref _isReceivingOrders, value);
                ModifyIsReceivingOrders();
            }
        }
        public Orders SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadNewOrdersAsync();
        }
        private void RefreshData()
        {
            LoadNewOrdersAsync();
            IsRefreshing = false;
        }
        private async void LoadEstablishmentAsync()
        {
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
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            Response response = await _apiService.GetEstablishmentByIdAsync(Constants.urlBase, Constants.servicePrefix, Constants.GetEstablishmentByIdAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

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
            IsReceivingOrders = EstablishmentResponse.Result.Receive;
            CheckActivateReceivingOrder();
        }
        
        private async void LoadNewOrdersAsync()
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

            NewOrders = new ObservableCollection<Orders>(response.Result.Where(x => x.ConsumptionTypeId == (int)ConsumptionTypes.Delivery && x.TrackingStatusId == (int)TrackingStatus.NewOrder).ToList());

            IsRunning = false;
            IsEnabled = true;
        }
        private async void OrderDetails(Orders obj)
        {
            if (SelectedOrder == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.order, SelectedOrder }
            };
            await _navigationService.NavigateAsync(nameof(OrderDetailPage), parameters);
            SelectedOrder = null;
        }
        private void ScheduleThirtyMinutes()
        {
            Settings.DeactivateReceivingOrderDate = JsonConvert.SerializeObject(DateTime.Now);
            Settings.ActivateReceivingOrderDate = JsonConvert.SerializeObject(DateTime.Now.AddMinutes(30));
            DeactivateReceivingOrder();
            CheckActivateReceivingOrder();
            
        }
        private void ScheduleToday()
        {
            Settings.DeactivateReceivingOrderDate = JsonConvert.SerializeObject(DateTime.Now);
            Settings.ActivateReceivingOrderDate = JsonConvert.SerializeObject(DateTime.Now.AddHours(24));
            DeactivateReceivingOrder();
            CheckActivateReceivingOrder();
            
        }
        private void CheckActivateReceivingOrder()
        {
            if (Settings.ActivateReceivingOrderDate == "" || Settings.DeactivateReceivingOrderDate == "")
            {
                return;
            }
            else
            {
                DateTime activate = JsonConvert.DeserializeObject<DateTime>(Settings.ActivateReceivingOrderDate);
                DateTime deactivate = JsonConvert.DeserializeObject<DateTime>(Settings.DeactivateReceivingOrderDate);

                if (activate.DayOfYear == deactivate.DayOfYear)
                {
                    var dueTime = activate - DateTime.Now;

                    if (dueTime.Ticks > 0)
                    {
                        var timer = new System.Threading.Timer(_ => ActivateReceivingOrder(), null, dueTime, TimeSpan.Zero);
                    }
                    else
                    {
                        ActivateReceivingOrder();
                    }
                }
                else
                {
                    if (activate.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        ActivateReceivingOrder();
                    }
                    else
                    {
                        var dueTime = activate - DateTime.Now;

                        if (dueTime.Ticks > 0)
                        {
                            var timer = new System.Threading.Timer(_ => ActivateReceivingOrder(), null, dueTime, TimeSpan.Zero);
                        }
                        else
                        {
                            ActivateReceivingOrder();
                        }
                    }
                }
            }
            
            
        }
        private void ActivateReceivingOrder()
        {
            IsReceivingOrders = true;

        }
        private void DeactivateReceivingOrder()
        {
            IsReceivingOrders = false;
        }
        private async void ModifyIsReceivingOrders()
        {
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
            Response response = await _apiService.UpdateEstablishmentReceive(url, Constants.servicePrefix, Constants.UpdateEstablishmentReceiveController, EstablishmentResponse.Result.Id, IsReceivingOrders, "bearer", token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }
        }
    }
}
