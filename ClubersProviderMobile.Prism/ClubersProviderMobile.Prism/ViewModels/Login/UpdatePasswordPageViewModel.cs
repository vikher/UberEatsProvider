using ClubersProviderMobile.Prism.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class UpdatePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private DelegateCommand _updatePasswordCommand;

        private string _password;
        private string _passwordConfirm;

        public UpdatePasswordPageViewModel(INavigationService navigationService,
                                         IPageDialogService dialogService,
                                         IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            Title = "Actualizar contraseña";
        }
        public DelegateCommand UpdatePasswordCommand => _updatePasswordCommand ?? (_updatePasswordCommand = new DelegateCommand(UpdatePasswordAsync));

        public string Email { get; set; }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set => SetProperty(ref _passwordConfirm, value);
        }


        private async Task<bool> ValidateDataAsync()
        {

            if (string.IsNullOrEmpty(Password))
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.enterPassword, Constants.cancelButton);
                return false;
            }

            if (Password.Length < 8)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.charactersPassword, Constants.cancelButton);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.confirmPassword, Constants.cancelButton);
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, Constants.enterConfirmPassword, Constants.cancelButton);
                return false;
            }
            return true;
        }
        private async void UpdatePasswordAsync()
        {

            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.titleOk, Constants.connectionError, Constants.cancelButton);
                return;
            }

            IsRunning = false;

            await _dialogService.DisplayAlertAsync(Constants.titleOk, Constants.updateSuccess, Constants.cancelButton);
            await NavigationService.NavigateAsync("/NavigationPage/LoginPage");

        }
    }
}
