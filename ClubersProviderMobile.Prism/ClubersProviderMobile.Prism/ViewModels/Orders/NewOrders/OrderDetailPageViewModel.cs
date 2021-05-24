using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class OrderDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;


        private ObservableCollection<OrderProduct> _products;
        private Orders _order;
        private Customer _Customer;    
        private bool _isVisibleAccept = false;
        private bool _isVisibleInProcess = false;
        private DelegateCommand _isVisibleCommand;

        public OrderDetailPageViewModel(INavigationService navigationService,
                                       IPageDialogService dialogService,
                                       IApiService apiService) : base(navigationService)
        {
            Title = "Mis pedidos";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public DelegateCommand IsVisibleCommand => _isVisibleCommand ?? (_isVisibleCommand = new DelegateCommand(IsVisible));
        #region Properties
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Orders Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public Customer Customer
        {
            get => _Customer;
            set => SetProperty(ref _Customer, value);
        }
        public bool IsVisibleAccept
        {
            get => _isVisibleAccept;
            set => SetProperty(ref _isVisibleAccept, value);
        }
        public bool IsVisibleInProcess
        {
            get => _isVisibleInProcess;
            set => SetProperty(ref _isVisibleInProcess, value);
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
        private async void IsVisible()
        {

            UpdateTrackingStatusAsync();
            await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/OrdersTabbedPage?selectedTab=OrdersInProcessPage");
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
            Response response = await _apiService.UpdateTrackingStatus(url, Constants.servicePrefix, Constants.UpdateTrackingStatusController, Order.Id, (int)TrackingStatus.InProcess, Constants.tokenType, token.Result.token);

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
