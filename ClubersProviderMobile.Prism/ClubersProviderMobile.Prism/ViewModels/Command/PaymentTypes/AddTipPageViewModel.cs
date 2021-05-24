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
using System.Linq.Expressions;
using System.Xml.Schema;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class AddTipPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Order _order;
        private Customer _customer;
        private EstablishmentStaff _collaborator;
        private List<Product> _products;
        private List<Tip> _tips;
        private Tip _tip;
        private int _quantityProducts;
        private decimal _totalPayable;

        private DelegateCommand _goPayRegCommand;

        public AddTipPageViewModel(INavigationService navigationService,
                                   IPageDialogService dialogService,
                                   IApiService apiService) : base(navigationService)
        {
            Title = "Agregar propina";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            LoadTipsAsync();
        }
        public DelegateCommand GoPaymentRegCommand => _goPayRegCommand ?? (_goPayRegCommand = new DelegateCommand(GoAddTipAsync));
        public List<Tip> Tips
        {
            get => _tips;
            set => SetProperty(ref _tips, value);
        }
        public Tip Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }
        public List<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Order Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public int QuantityProducts
        {
            get => _quantityProducts;
            set => SetProperty(ref _quantityProducts, value);
        }
        public decimal TotalPayable
        {
            get => _totalPayable;
            set => SetProperty(ref _totalPayable, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Order>(Constants.order);
                Products = Order.Products;
                foreach (var item in Products)
                {
                    QuantityProducts = item.Quantity + QuantityProducts;
                    TotalPayable = item.Price + TotalPayable;
                }
                Collaborator = Order.Collaborator;
            }
        }
        private async void LoadTipsAsync()
        {
            Response response = await _apiService.GetTipsAsync<Tip>("", " / api", "/Establishment", "bearer", "");
            Tips = (List<Tip>)response.Result;
        }
        private async void GoAddTipAsync()
        {
            var parameters = new NavigationParameters
            {
                { Constants.order, Order},
                { Constants.tip, Tip}
            };
            await _navigationService.NavigateAsync(nameof(AddedTipPage), parameters);
        }
    }
}
