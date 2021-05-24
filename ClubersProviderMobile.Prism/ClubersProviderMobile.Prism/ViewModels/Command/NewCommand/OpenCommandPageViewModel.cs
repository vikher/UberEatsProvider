using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class OpenCommandPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Customer _customer;        

        private DelegateCommand _selectMenuCommand;
        public OpenCommandPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nueva Comanda";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand GoCollaboratorPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.customer))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customer>(Constants.customer);
                IsRunning = false;
            }
        }
        private async void SelectMenuAsync()
        {
            if (Customer == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.customer, Customer }
            };
            await _navigationService.NavigateAsync(nameof(CollaboratorPage), parameters);
        }
    }
}
