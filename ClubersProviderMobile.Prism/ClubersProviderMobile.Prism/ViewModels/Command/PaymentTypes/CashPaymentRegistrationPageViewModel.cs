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
    public class CashPaymentRegistrationPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private DelegateCommand _goToHomeCommand;

        private Orders _order;

        public CashPaymentRegistrationPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Registro pago efectivo";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public DelegateCommand GoToHomeCommand => _goToHomeCommand ?? (_goToHomeCommand = new DelegateCommand(GoToHomeAsync));

        public Orders Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Orders>(Constants.order);
            }
        }

        private async void GoToHomeAsync()
        {
            await NavigationService.NavigateAsync("/ProviderMasterDetailPage/NavigationPage/HistoricOrdersTabbedPage?selectedTab=OnSiteOrdersPage");
        }
    }
}
