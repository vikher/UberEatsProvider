using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using Prism.Commands;
using Prism.Navigation;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PageName == "LoginPage")
            {
                if (Settings.IsLogin)
                {
                    Settings.IsLogin = false;
                    Settings.User = null;
                    Settings.Token = null;
                }
                await _navigationService.NavigateAsync($"/NavigationPage/{PageName}");
            }
            else
            {
                await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/{PageName}");
            }
            
        }
    }

}
