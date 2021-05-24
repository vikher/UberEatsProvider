using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class MenuAvailabilityPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private DelegateCommand<Products> _toggledCommand;
        private DelegateCommand _refreshViewCommand;
        private bool _isRefreshing;
        private bool _isRunning;
        private Products? _selectedProduct;

        private ObservableCollection<Products> _products;
        private ObservableCollection<Availability> _availabilityList;

        public MenuAvailabilityPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Disponibilidad de menú";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand<Products> ToggledCommand => _toggledCommand ?? (_toggledCommand = new DelegateCommand<Products>(SwitchToggledAsync));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));

        public ObservableCollection<Availability> AvailabilityList
        {
            get => _availabilityList;
            set => SetProperty(ref _availabilityList, value);
        }

        private Availability _availabilityelement;

        public Availability AvailabilityElement
        {
            get => _availabilityelement;
            set => SetProperty(ref _availabilityelement, value);
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        public Products? SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        public ObservableCollection<Products> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            LoadProductsAsync();

        }

        private async void LoadProductsAsync()
        {
            LoadAvailabilityListAsync();

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

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetAllProductsByEstablishmentIDAsync(url, Constants.servicePrefix, Constants.GetAllProductsByEstablishmentIDAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

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

            ProductsResponse produtsResponse = (ProductsResponse)response.Result;
            Products = new ObservableCollection<Products>(produtsResponse.Result);
        }

        private async void LoadAvailabilityListAsync()
        {
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.connectionError, Constants.cancelButton);
                return;
            }

            //Response response = await _apiService.GetAvailavilityListAsync<Availability>(Constants.urlBase, Constants.servicePrefix, Constants.controller, Constants.tokenType, Constants.accessToken);

            //var availabilityList = (List<Availability>)response.Result;
          
        }
        private async void SwitchToggledAsync(Products product)
        {
            SelectedProduct.Available = !SelectedProduct.Available;

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
            Response response = await _apiService.UpdateProductAvailable(url, Constants.servicePrefix, Constants.UpdateProductAvailableController, SelectedProduct.Id, SelectedProduct.Available, Constants.tokenType, token.Result.token);

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
            LoadProductsAsync();
        }
        private void RefreshData()
        {
            LoadProductsAsync();
            IsRefreshing = false;
        }
    }
}
