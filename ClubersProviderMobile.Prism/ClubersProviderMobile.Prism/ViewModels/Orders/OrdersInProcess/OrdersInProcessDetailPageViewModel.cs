using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
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
    public class OrdersInProcessDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private ObservableCollection<OrderProduct> _products;
        private Orders _orderDetail;
        private Customer _Customer;
        private DeliveryMan _deliveryMan;
        private bool _changeOrderInProcess = false;
        private string _chatIcon = IconFont.Message;
        private DelegateCommand<bool> _orderInProcessCommand;
        private DelegateCommand _numberOfPackagesCommand;
        private DelegateCommand _chatCommand;

        public OrdersInProcessDetailPageViewModel(INavigationService navigationService,
                                       IPageDialogService dialogService,
                                       IApiService apiService) : base(navigationService)
        {
            Title = "Detalle de pedido";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public DelegateCommand<bool> ChangeOrderInProcessCommand => _orderInProcessCommand ?? (_orderInProcessCommand = new DelegateCommand<bool>(ChangeEstatusOrderInProcess));
        public DelegateCommand NumberOfPackagesCommand => _numberOfPackagesCommand ?? (_numberOfPackagesCommand = new DelegateCommand(NumberOfPackagesAsync));
        public DelegateCommand ChatCommand => _chatCommand ?? (_chatCommand = new DelegateCommand(ChatAsync));


        #region Properties
        public string ChatIcon
        {
            get => _chatIcon;
            set => SetProperty(ref _chatIcon, value);
        }
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Orders Order
        {
            get => _orderDetail;
            set => SetProperty(ref _orderDetail, value);
        }
        public Customer Customer
        {
            get => _Customer;
            set => SetProperty(ref _Customer, value);
        }
        public DeliveryMan DeliveryMan
        {
            get => _deliveryMan;
            set => SetProperty(ref _deliveryMan, value);
        }
        public bool ChangeOrderInProcess
        {
            get => _changeOrderInProcess;
            set => SetProperty(ref _changeOrderInProcess, value);
        }
        #endregion Properties
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.order))
            {
                IsRunning = true;
                Order = parameters.GetValue<Orders>(Constants.order);
                Products = new ObservableCollection<OrderProduct>(Order?.OrderProducts);

                IsRunning = false;
            }
        }
        private async void ChangeEstatusOrderInProcess(bool obj)
        {
            ChangeOrderInProcess = true;
        }

        private async void NumberOfPackagesAsync()
        {

            if (Order == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.order, Order }
            };
            await _navigationService.NavigateAsync(nameof(NumberOfPackagesPage), parameters);
        }

        private async void ChatAsync()
        {
            await _navigationService.NavigateAsync(nameof(MessagesPage));
        }
    }
}
