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
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ApproveDishPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private int _trackingStatus;
        private ObservableCollection<OrderProduct> _products;
        private Orders _orderDetail;
        private Customer _Customer;
        private EstablishmentStaff _collaborator;
        private OrderProduct _selectProduct;
        private decimal _total = 0;
        private DelegateCommand _requestReviewCommand;

        private DelegateCommand _selectMenuCommand;

        private DelegateCommand<OrderProduct> _productDetailCommand;
        public ApproveDishPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Pago desde app";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public DelegateCommand<OrderProduct> ProductDetailCommand => _productDetailCommand ?? (_productDetailCommand = new DelegateCommand<OrderProduct>(ProductDetails));
        public DelegateCommand GoEstablishmentPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        public DelegateCommand RequestReviewCommand => _requestReviewCommand ?? (_requestReviewCommand = new DelegateCommand(RequestReviewAsync));

        
        #region Properties
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public int TrackingStatus
        {
            get => _trackingStatus;
            set => SetProperty(ref _trackingStatus, value);
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
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public OrderProduct SelectProduct
        {
            get => _selectProduct;
            set => SetProperty(ref _selectProduct, value);
        }
        #endregion Properties
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
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
        private async void ProductDetails(OrderProduct product)
        {
            var parameters = new NavigationParameters
            {
                { Constants.product, product }
            };
            await _navigationService.NavigateAsync(nameof(ApproveDishDetailPage), parameters);
        }

        private async void SelectMenuAsync()
        {
            if (Order == null)
            {
                IsRunning = true;
                await _navigationService.GoBackAsync();
                IsRunning = false;
            }
            else
            {
                var parameters = new NavigationParameters
                {
                    { Constants.products, Products},
                    { Constants.customer, Customer},
                    { Constants.order, Order},
                    { Constants.collaborator, Collaborator},
                };
                await _navigationService.NavigateAsync(nameof(EstablishmentPage), parameters);
            }
        }
        private async void RequestReviewAsync()
        {
            TrackingStatus = 4;
            UpdateTrackingStatusAsync();
            await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/CommandTabbedPage?selectedTab=ReviewCommandPage");

        }
        private async void UpdateTrackingStatusAsync()
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
            Response response = await _apiService.UpdateTrackingStatus(url, Constants.servicePrefix, Constants.UpdateTrackingStatusController, Order.Id, TrackingStatus, Constants.tokenType, token.Result.token);

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
