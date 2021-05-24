using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class NumberOfPackagesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private Orders _orderDetail;
        private int _numberOfPackagesDelivered;
		private DelegateCommand _readyCommand;
        public NumberOfPackagesPageViewModel(INavigationService navigationService,
            IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            Title = "NumberOfPackages";
            IsEnabled = false;
        }

        public DelegateCommand ReadyCommand => _readyCommand ?? (_readyCommand = new DelegateCommand(ReadyAsync));

        public Orders Order
        {
            get => _orderDetail;
            set => SetProperty(ref _orderDetail, value);
        }
        public int NumberOfPackagesDelivered
        {
            get => _numberOfPackagesDelivered;
            set 
            {
                SetProperty(ref _numberOfPackagesDelivered, value);
                if (value != null && value != 0)
                {
                    IsEnabled = true;
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.order))
            {
                IsRunning = true;
                Order = parameters.GetValue<Orders>(Constants.order);

                IsRunning = false;
            }
        }
        private async void ReadyAsync()
        {

            PutNumberOfPackagesAsync();
            UpdateTrackingStatusAsync();
            await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/OrdersTabbedPage?selectedTab=ReadyOrdersPage");
        }
        private async void PutNumberOfPackagesAsync()
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
            Response response = await _apiService.PutNumberOfPackagesDeliveredAsync(url, Constants.servicePrefix, Constants.PutNumberOfPackagesDeliveredAsyncController, Order.Id, NumberOfPackagesDelivered, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }

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
            Response response = await _apiService.UpdateTrackingStatus(url, Constants.servicePrefix ,Constants.UpdateTrackingStatusController, Order.Id, (int)TrackingStatus.Ready, Constants.tokenType, token.Result.token);

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