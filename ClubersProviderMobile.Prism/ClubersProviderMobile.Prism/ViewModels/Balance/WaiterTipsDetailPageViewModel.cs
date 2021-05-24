using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Navigation;
using System.Collections.Generic;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class WaiterTipsDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<Order> orders;
        private EstablishmentStaff _collaborator;
        private bool _isRunning;

        public WaiterTipsDetailPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Consulta de Saldos";
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }

        public List<Order> Orders
        {
            get => orders;
            set => SetProperty(ref orders, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.collaborator))
            {
                EstablishmentStaffDetail collaborator = parameters.GetValue<EstablishmentStaffDetail>(Constants.collaborator);
                Collaborator = collaborator.EstablishmentStaff;
            }
        }
    }
}