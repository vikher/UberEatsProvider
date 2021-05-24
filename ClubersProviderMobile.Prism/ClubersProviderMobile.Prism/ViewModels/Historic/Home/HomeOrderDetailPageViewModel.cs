using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class HomeOrderDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private Orders _order;
        private Customer _customer;
        private ObservableCollection<OrderProduct> _products;
        private EstablishmentStaff _collaborator;

        public HomeOrderDetailPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Detalle Domicilio";
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public Orders Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Orders>(Constants.order);
                Products = new ObservableCollection<OrderProduct>(Order?.OrderProducts);

            }
        }
    }
}