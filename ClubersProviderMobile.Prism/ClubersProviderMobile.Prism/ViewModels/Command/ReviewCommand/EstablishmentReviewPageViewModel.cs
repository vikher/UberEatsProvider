using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
using ClubersProviderMobile.Prism.Views.Command;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class EstablishmentReviewPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private string _comments;
        private int _quantity;

        private string _search;
        private Establishment _establishment;
        private List<Products> _myproducts;
        private Products _selectedProduct;
        private Customers _customer;
        private EstablishmentStaff _collaborator;
        private Orders _order;
        ObservableCollection<OrderProduct> Products = new ObservableCollection<OrderProduct>();

        private ObservableCollection<Grouping<string, Products>> _ProductsGrouped;

        private DelegateCommand _searchCommand;
        private DelegateCommand _selectMenuCommand;
        private DelegateCommand<Products> _selectProductCommand;
        private ObservableCollection<Products> _testProducts;
        public EstablishmentReviewPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Establecimiento";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowProducts));
        public DelegateCommand<Products> SelectedProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand<Products>(ProductDetails));
        public DelegateCommand GoDetailCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        public Establishment Establishment
        {
            get => _establishment;
            set => SetProperty(ref _establishment, value);
        }
        public ObservableCollection<Grouping<string, Products>> ProductsGrouped
        {
            get => _ProductsGrouped;
            set => SetProperty(ref _ProductsGrouped, value);
        }
        public string Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public Products SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        public Customers Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public Orders Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowProducts();
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("comments"))
            {
                IsRunning = true;
                Comments = parameters.GetValue<string>("comments");
                IsRunning = false;
            }
            if (parameters.ContainsKey("quantity"))
            {
                IsRunning = true;
                Quantity = parameters.GetValue<int>("quantity");
                IsRunning = false;
            }
            if (parameters.ContainsKey(Constants.customer))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customers>(Constants.customer);
                Collaborator = parameters.GetValue<EstablishmentStaff>(Constants.collaborator);
                Order = parameters.GetValue<Orders>(Constants.order);
                Products = new ObservableCollection<OrderProduct>(Order?.OrderProducts);
                IsRunning = false;
            }
            else if (parameters.ContainsKey(Constants.product))
            {
                IsRunning = true;
                _selectedProduct = parameters.GetValue<Products>(Constants.product);
                Products.Add(new OrderProduct() { Id = Order.Id, OrderId = Order.Id, Created = DateTime.Now, CreatedById = Order.CustomerId, Modified = DateTime.Now, ModifiedById = Order.CustomerId, StatusId = 1, ProductId = _selectedProduct.Id, OrdersProductsComponents = new List<OrderProductComponent>() { }, OrdersProductsSubsections = new List<OrderProductSubsection> { },  Product = _selectedProduct,  Comments = Comments, Quantity = Quantity });
                IsRunning = false;
            }

            LoadProductsAsync();
        }
        
        private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                var sorted = from Product in _myproducts
                             where Product.ProductSection.Any()
                             orderby Product.Name
                             group Product by Product.ProductSection.ToArray().LastOrDefault().Section.Name into ProductGroup
                             select new Grouping<string, Products>(ProductGroup.Key, ProductGroup);
                //create a new collection of groups
                ProductsGrouped = new ObservableCollection<Grouping<string, Products>>(sorted);
            }
            else
            {
                var sorted = from Product in _myproducts
                             where Product.ProductSection.Any() && Product.Name.ToUpper().Contains(Search.ToUpper())
                             orderby Product.Name
                             group Product by Product.ProductSection.ToArray().LastOrDefault().Section.Name into ProductGroup
                             select new Grouping<string, Products>(ProductGroup.Key, ProductGroup);
                //create a new collection of groups
                ProductsGrouped = new ObservableCollection<Grouping<string, Products>>(sorted);
            }
        }
        private async void ProductDetails(Products obj)
        {
            if (SelectedProduct == null)
                return;
            NavigationParameters parameters = new NavigationParameters
            {
                { Constants.product, SelectedProduct }
            };

            await _navigationService.NavigateAsync(nameof(ProductDetailPage), parameters);
            SelectedProduct = null;
        }
        private async void SelectMenuAsync()
        {
            IsRunning = true;
            if (Products == null)
            {
                IsRunning = false;
                return;
            }
            Order.OrderProducts = Products;
            var parameters = new NavigationParameters
                {
                    { Constants.customer, Customer},
                    { Constants.collaborator, Collaborator},
                    { Constants.order, Order}
                };
            await _navigationService.NavigateAsync(nameof(ReviewCommandDetailPage), parameters);
            //SelectedCollaborator = null;
            IsRunning = false;
        }
        private async void LoadProductsAsync()
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
            _myproducts = produtsResponse.Result;
            ShowProducts();
        }
    }
}
