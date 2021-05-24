using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CollaboratorPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private Customers _customer;
        private Orders _order;
        private List<EstablishmentStaff> _mycollaborators;
        private ObservableCollection<EstablishmentStaff> _collaborators;
        private EstablishmentStaff _selectedCollaborator;
        private string _search;

        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _searchCommand;
        private DelegateCommand<EstablishmentStaff> _selectMenuCommand;
        public CollaboratorPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Agregar mesero";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;            
        }
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCollaborators));
        public DelegateCommand<EstablishmentStaff> GoToCollaboratorAddedPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand<EstablishmentStaff>(SelectMenuAsync));
        public Customers Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public EstablishmentStaff SelectedCollaborator
        {
            get => _selectedCollaborator;
            set => SetProperty(ref _selectedCollaborator, value);
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
                ShowCollaborators();
            }
        }
        public ObservableCollection<EstablishmentStaff> Collaborators
        {
            get => _collaborators;
            set => SetProperty(ref _collaborators, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsRunning = true;
            if (parameters.ContainsKey(Constants.customer))
            {
                Customer = parameters.GetValue<Customers>(Constants.customer);
            }
            if (parameters.ContainsKey(Constants.order))
            {
                Order = parameters.GetValue<Orders>(Constants.order);
            }
            IsRunning = false;
            LoadCollaboratorsAsync();
        }
        private void RefreshData()
        {
            LoadCollaboratorsAsync();
            IsRefreshing = false;
        }
        private async void LoadCollaboratorsAsync()
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
            Response response = await _apiService.GetAllCollaboratorByEstablishmentIDAsync(url, Constants.servicePrefix, Constants.GetAllCollaboratorByEstablishmentIDAsyncController, user.Result.Establishment.Id, Constants.tokenType, token.Result.token);

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
            CollaboratorResponse CollaboratorResponse = (CollaboratorResponse)response.Result;
            _mycollaborators = CollaboratorResponse.Result;
            ShowCollaborators();
        }
        private void ShowCollaborators()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Collaborators = new ObservableCollection<EstablishmentStaff>(_mycollaborators);
            }
            else
            {
                Collaborators = new ObservableCollection<EstablishmentStaff>(_mycollaborators.Where(c => c.FullName.ToUpper().Contains(Search.ToUpper())));
            }
        }
        private async void SelectMenuAsync(EstablishmentStaff obj)
        {
            IsRunning = true;
            if (Customer == null || SelectedCollaborator == null)
                return;
            if (Order == null)
            {
                var parameters = new NavigationParameters
                {
                    { Constants.customer, Customer},
                    { Constants.collaborator, SelectedCollaborator }
                };
                await _navigationService.NavigateAsync(nameof(PurchaseDetailPage), parameters);
                SelectedCollaborator = null;
                IsRunning = false;
            }
            else 
            {
                var parameters = new NavigationParameters
                {
                    { Constants.order, Order},
                    { Constants.customer, Customer},
                    { Constants.collaborator, SelectedCollaborator},                    
                };
                await _navigationService.NavigateAsync(nameof(PurchaseDetailPage), parameters);
                SelectedCollaborator = null;
                IsRunning = false;
            }            
        }
    }
}
