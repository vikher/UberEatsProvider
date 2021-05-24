 using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class NewCommandPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private DelegateCommand _selectMenuCommand;
        public NewCommandPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nuevas";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        public async void SelectMenuAsync()
        {
            IsRunning = false;
            await _navigationService.NavigateAsync(nameof(ScanQrPage));
        }
    }
}
