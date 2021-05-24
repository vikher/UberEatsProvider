using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CashPaymentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Order _order;
        private Customer _customer;
        private EstablishmentStaff _collaborator;
        private List<Product> _products;

        private DelegateCommand _addTipPageCommand;
        public CashPaymentPageViewModel(INavigationService navigationService,
                                      IPageDialogService dialogService,
                                      IApiService apiService) : base(navigationService)
        {
            Title = "Pago con efectivo";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
        }
        public DelegateCommand GoAddTipCommand => _addTipPageCommand ?? (_addTipPageCommand = new DelegateCommand(GoAddTipAsync));
        
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
        public List<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsRunning = true;
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Order>(Constants.order);
                Products = Order.Products;
                Customer = Order.Customer;
                Collaborator = Order.Collaborator;
            }
            IsRunning = false;
        }
        private async void GoAddTipAsync()
        {
            var parameters = new NavigationParameters
            {
                { Constants.order, Order }
            };
            await _navigationService.NavigateAsync(nameof(AddTipPage), parameters);
        }
    }
}
