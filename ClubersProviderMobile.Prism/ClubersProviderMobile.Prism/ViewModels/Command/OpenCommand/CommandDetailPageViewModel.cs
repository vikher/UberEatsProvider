using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using ClubersProviderMobile.Prism.Views.Command.OpenCommand;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CommandDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private ObservableCollection<OrderProduct> _products;
        private Orders _orderDetail;
        private Customer _Customer;
        private EstablishmentStaff _collaborator;
        private bool _isVisibleCloseCommand;
        private bool _isVisiblePayment;
        private float _total;
        private Tips _selectedTip;
        private TipResponse _tipResponse;
        private int _trackingStatus;

        private List<Tip> _tips;

        private DelegateCommand _customTipCommand;
        private DelegateCommand _isVisibleCommand;
        private DelegateCommand<OrderProduct> _removeProductsCommand;
        private DelegateCommand _paymentAppCommand;
        private DelegateCommand _paymentCashCommand;
        private DelegateCommand _goPayRegCommand;

        public CommandDetailPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Detalle de comanda";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            LoadTipsAsync();
            SelectedTip.Amount = 0;
        }
        public DelegateCommand CustomTipCommand => _customTipCommand ?? (_customTipCommand = new DelegateCommand(ShowCustomTipAsync));
        public DelegateCommand GoPaymentRegCommand => _goPayRegCommand ?? (_goPayRegCommand = new DelegateCommand(GoAddTipAsync));
        private DelegateCommand _requestReviewCommand;

        public DelegateCommand RequestReviewCommand => _requestReviewCommand ?? (_requestReviewCommand = new DelegateCommand(RequestReviewAsync));

        private async void RequestReviewAsync()
        {
            TrackingStatus = 4;
            UpdateTrackingStatusAsync();
            await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/CommandTabbedPage?selectedTab=ReviewCommandPage");

        }

        public DelegateCommand IsVisibleCommand => _isVisibleCommand ?? (_isVisibleCommand = new DelegateCommand(IsVisibleAsync));
        public DelegateCommand<OrderProduct> DeleteCommand => _removeProductsCommand ?? (_removeProductsCommand = new DelegateCommand<OrderProduct>(RemoveProductsAsync));
        public DelegateCommand PaymentAppCommand => _paymentAppCommand ?? (_paymentAppCommand = new DelegateCommand(GoPaymentAppAsync));
        public DelegateCommand CashPaymentCommand => _paymentCashCommand ?? (_paymentCashCommand = new DelegateCommand(GoCashPaymentAsync));
        #region Properties
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public TipResponse TipResponse
        {
            get => _tipResponse;
            set => SetProperty(ref _tipResponse, value);
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
        /*public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }*/

        public float Total
        {
            get
            {
                if (Order == null)
                {
                    _total = 0;
                }
                else
                {
                    _total = (Products.Sum(x => x.Subtotal) + SelectedTip.Amount);
                }
                return _total;
            }

            set => SetProperty(ref _total, value);
        }
        public bool IsVisibleCloseCommand
        {
            get => _isVisibleCloseCommand;
            set => SetProperty(ref _isVisibleCloseCommand, value);
        }
        public bool IsVisiblePayment
        {
            get => _isVisiblePayment;
            set => SetProperty(ref _isVisiblePayment, value);
        }
        
        public Tips SelectedTip
        {
            get => _selectedTip;
            set => SetProperty(ref _selectedTip, value, () => RaisePropertyChanged(nameof(Total)));
        }
        public List<Tip> Tips
        {
            get => _tips;
            set => SetProperty(ref _tips, value);
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
                Total = (Products.Sum(x => x.Subtotal) + SelectedTip.Amount);

                IsRunning = false;
            }

            if (parameters.ContainsKey("customTip"))
            {
                SelectedTip = new Tips() { Amount = float.Parse(parameters.GetValue<string>("customTip")) };
                Order.Tip.Amount = SelectedTip.Amount;
            }
        }
        private void IsVisibleAsync()
        {
            IsVisibleCloseCommand = false;
            IsVisiblePayment = true;
        }
        private async void RemoveProductsAsync(OrderProduct product)
        {
            Total = 0;
            Products.Remove(product);
            _total = (Order.OrderProducts.Sum(x => x.Subtotal) + SelectedTip.Amount);

        }
        private async void GoPaymentAppAsync()
        {
            Order.OrderProducts = Products;
            Order.Tip = SelectedTip;
            Order.Total = Total;
            var parameters = new NavigationParameters
            {
                { Constants.order, Order }
            };
            await _navigationService.NavigateAsync(nameof(PaymentAppPage), parameters);
        }
        private async void GoCashPaymentAsync()
        {
            Order.OrderProducts = Products;
            Order.Tip = SelectedTip;
            Order.Total = Total;
            var parameters = new NavigationParameters
            {
                { Constants.order, Order }
            };
            await _navigationService.NavigateAsync(nameof(CashPaymentPage), parameters);
        }

        private async void ShowCustomTipAsync()
        {
            await _navigationService.NavigateAsync(nameof(CustomTipPage));
        }
        private async void LoadTipsAsync()
        {
            Response response = await _apiService.GetTipsAsync<Tip>("", " / api", "/Establishment", "bearer", "");
            Tips = (List<Tip>)response.Result;
            SelectedTip = new Tips() { Amount = 0 };

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response2 = await _apiService.GetTipsByEstablishmentIDAsync(url, Constants.servicePrefix, Constants.GetTipsByEstablishmentIDAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response2.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }

            TipResponse = (TipResponse)response2.Result;        
        }
        private async void GoAddTipAsync()
        {
            Order.OrderProducts = Products;
            Order.Tip = SelectedTip;
            Order.Total = Total;
            var parameters = new NavigationParameters
            {
                { Constants.order, Order}
            };
            TrackingStatus = 3;
            UpdateTrackingStatusAsync();
            await _navigationService.NavigateAsync(nameof(CashPaymentRegistrationPage), parameters);
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
