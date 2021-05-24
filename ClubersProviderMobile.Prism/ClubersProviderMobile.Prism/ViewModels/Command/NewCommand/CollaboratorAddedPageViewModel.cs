using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CollaboratorAddedPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private string _customerIcon = IconFont.Account;

        private Customers _customer;
        private EstablishmentStaff _collaborator;

        private DelegateCommand _selectMenuCommand;
        public CollaboratorAddedPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nueva comanda";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand GoEstablishmentPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
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
        public EstablishmentStaff Collaborator
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.customer))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customers>(Constants.customer);
                Collaborator = parameters.GetValue<EstablishmentStaff>(Constants.collaborator);
                IsRunning = false;
            }
        }
        private async void SelectMenuAsync()
        {
            IsRunning = true;
            if (Customer == null && Collaborator == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.customer, Customer},
                { Constants.collaborator, Collaborator}
            };
            await _navigationService.NavigateAsync(nameof(EstablishmentPage), parameters);
            IsRunning = false;
        }
    }
}
