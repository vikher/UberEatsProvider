using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ApproveDishDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Product _product;
        public ApproveDishDetailPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Detalle";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.product))
            {
                IsRunning = true;
                Product = parameters.GetValue<Product>(Constants.product);
                IsRunning = false;
            }
        }
    }
}
