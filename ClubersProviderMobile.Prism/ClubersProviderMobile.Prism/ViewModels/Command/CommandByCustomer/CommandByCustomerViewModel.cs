using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CommandByCustomerViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private string _customerIcon = IconFont.Account;

        private Customers _customer;
        private Orders? _order;
        private ObservableCollection<OrderProduct> _product;
        private List<OrderProduct> _myproducts;
        private float? _total;
        private OrderProduct? _selectProduct;

        private DelegateCommand _valueChangedCommand;
        private DelegateCommand _selectMenuCommand;
        private DelegateCommand<OrderProduct> _removeProductsCommand;
        public CommandByCustomerViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nueva comanda";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;            
        }
        public DelegateCommand ValueChangedCommand => _valueChangedCommand ?? (_valueChangedCommand = new DelegateCommand(StepperValueChangedAsync));
        public DelegateCommand GoCollaboratorPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        public DelegateCommand<OrderProduct> DeleteCommand => _removeProductsCommand ?? (_removeProductsCommand = new DelegateCommand<OrderProduct>(RemoveProductsAsync));
        public string CustomerIcon
        {
            get => _customerIcon;
            set => SetProperty(ref _customerIcon, value);
        }
        public Customers Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public Orders? Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public ObservableCollection<OrderProduct> Products
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }
        public OrderProduct? SelectProduct
        {
            get => _selectProduct;
            set => SetProperty(ref _selectProduct, value);
        }
        public float? Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.customer))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customers>(Constants.customer);

                if (Customer.Orders != null && Customer.Orders.Count() != 0)
                {
                    Order = Customer.Orders.Where(x => x.ConsumptionTypeId == 3 && x.TrackingStatusId == 1).FirstOrDefault();
                    if (Order != null)
                    {
                        Products = new ObservableCollection<OrderProduct>(Order?.OrderProducts);
                        Total = Products?.Sum(x => x.Subtotal);
                    }
                    
                }
               
                IsRunning = false;
            }
        }
        private void StepperValueChangedAsync()
        {
            LoadTotalPriceAsync();
        }
        private void LoadTotalPriceAsync()
        {
            if (Products != null)
            {
                Total = Products.Sum(x => x.Subtotal);
            }
            else
            {
                return;
            }
        }
        private async void SelectMenuAsync()
        {
            if (Customer == null)
                return;
            if (Order == null)
            {
                var parameters = new NavigationParameters
            {
                { Constants.customer, Customer }
            };
                await _navigationService.NavigateAsync(nameof(CollaboratorPage), parameters);
            }
            else
            { 
                Order.OrderProducts = Products;
                Order.Total = (float)Total;
                var parameters = new NavigationParameters
            {
                { Constants.customer, Customer },
                { Constants.order, Order}
            };
                await _navigationService.NavigateAsync(nameof(CollaboratorPage), parameters);
            }
           
        }
        private async void RemoveProductsAsync(OrderProduct product)
        {
            Total = 0;
            Products.Remove(product);
            Total = Products?.Sum(x => x.Subtotal);

        }
    }
}
