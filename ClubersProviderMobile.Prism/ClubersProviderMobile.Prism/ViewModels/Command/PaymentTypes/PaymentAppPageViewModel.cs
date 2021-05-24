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
    public class PaymentAppPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Order _order;
        private Customer _customer;
        private EstablishmentStaff _collaborator;
        public PaymentAppPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Pago desde app";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public Order Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsRunning = true;
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Order>(Constants.order);
                Customer = Order.Customer;
                Collaborator = Order.Collaborator;               
            }
            IsRunning = false;
        }
    }
}
