using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views;
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
    public class SearchWaiterPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<EstablishmentStaffDetail> _myCollaborators;
        private ObservableCollection<EstablishmentStaffDetail> _collaborator;
        private DateTime _startDate;
        private DateTime _endDate;
        private DelegateCommand _searchCommand;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _filterCommand;
        private DelegateCommand<EstablishmentStaffDetail> _selectedCollaboratorCommand;
        private EstablishmentStaffDetail _selectedCollaborator;
        private string _search;
        public SearchWaiterPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            Title = "Consulta de saldos";
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCollaborators));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));
        public DelegateCommand FilterCommand => _filterCommand ?? (_filterCommand = new DelegateCommand(LoadCollaboratorsAsync));
        public DelegateCommand<EstablishmentStaffDetail> CollaboratorSelectedCommand => _selectedCollaboratorCommand ?? (_selectedCollaboratorCommand = new DelegateCommand<EstablishmentStaffDetail>(CollaboratorDetails));
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        public ObservableCollection<EstablishmentStaffDetail> Collaborators
        {
            get => _collaborator;
            set => SetProperty(ref _collaborator, value);
        }
        public EstablishmentStaffDetail SelectedCollaborator
        {
            get => _selectedCollaborator;
            set => SetProperty(ref _selectedCollaborator, value);
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
        private void RefreshData()
        {
            LoadCollaboratorsAsync();
            IsRefreshing = false;
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadCollaboratorsAsync();
        }
        private async void LoadCollaboratorsAsync()
        {

            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync("Error", "Compruebe la conexión a Internet", "Aceptar");
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            CollaboratorDetailRequest request = new CollaboratorDetailRequest
            {
                EstablishmentId = user.Result.Establishment.Id,
                FechaInicio = StartDate.Date,
                FechaFin = EndDate.Date,
            };

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            EstablishmentStaffDetailResponse response = await _apiService.GetCollaboratorsDetalisByEstablishmentIDAsync(url, Constants.servicePrefix, Constants.GetCollaboratorsDetalisByEstablishmentIDAsyncController, Constants.tokenType, token.Result.token, request);

            IsRunning = false;
            IsEnabled = true;
            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.ResultMessages[0], "Aceptar");
                return;
            }

            _myCollaborators = response.Result;
            ShowCollaborators();
        }

        private async void CollaboratorDetails(EstablishmentStaffDetail obj)
        {
            if (SelectedCollaborator == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.collaborator, SelectedCollaborator }
            };
            await _navigationService.NavigateAsync(nameof(WaiterTipsDetailPage), parameters);
            SelectedCollaborator = null;
        }

        private void ShowCollaborators()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Collaborators = new ObservableCollection<EstablishmentStaffDetail>(_myCollaborators);
            }
            else
            {
                Collaborators = new ObservableCollection<EstablishmentStaffDetail>(_myCollaborators.Where(r => r.EstablishmentStaff.FullName.ToUpper().Contains(Search.ToUpper())));
            }
        }

    }
}
