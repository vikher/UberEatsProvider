using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Command;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class PurchaseDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private string _customerIcon = IconFont.Account;

        private Customers _customer;
        private Orders _order;
        private EstablishmentStaff _collaborator;
        private ObservableCollection<OrderProduct> _products;
        private float? _total;
        private bool _isProceedEnabled;
        private int _tableNumber;

        private DelegateCommand _valueChangedCommand;
        private DelegateCommand _selectMenuCommand;
        private DelegateCommand _goOpenCommand;
        private DelegateCommand<OrderProduct> _removeProductsCommand;
        public PurchaseDetailPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Nueva comanda";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }
        public DelegateCommand ValueChangedCommand => _valueChangedCommand ?? (_valueChangedCommand = new DelegateCommand(StepperValueChangedAsync));
        public DelegateCommand GoEstablishmentPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        public DelegateCommand GoOpenCommandsCommand => _goOpenCommand ?? (_goOpenCommand = new DelegateCommand(GoOpenCommand));
        public DelegateCommand<OrderProduct> DeleteCommand => _removeProductsCommand ?? (_removeProductsCommand = new DelegateCommand<OrderProduct>(RemoveProductsAsync));
        public string CustomerIcon
        {
            get => _customerIcon;
            set => SetProperty(ref _customerIcon, value);
        }
        public int TableNumber
        {
            get => _tableNumber;
            set => SetProperty(ref _tableNumber, value);
        }
        public bool IsProceedEnabled
        {
            get => _isProceedEnabled;
            set => SetProperty(ref _isProceedEnabled, value);
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
        public ObservableCollection<OrderProduct> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Orders Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public float? Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.customer) && !parameters.ContainsKey(Constants.order))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customers>(Constants.customer);
                Collaborator = parameters.GetValue<EstablishmentStaff>(Constants.collaborator);

                IsRunning = false;
            }
            else if(parameters.ContainsKey(Constants.order))
            {
                IsRunning = true;
                Customer = parameters.GetValue<Customers>(Constants.customer);
                Collaborator = parameters.GetValue<EstablishmentStaff>(Constants.collaborator);
                Order = parameters.GetValue<Orders>(Constants.order);

                if (Order?.OrderProducts != null)
                {
                    Products = new ObservableCollection<OrderProduct>(Order?.OrderProducts);
                    
                    Total = Products?.Sum(x => x.Subtotal);
                }
                if (Order?.TableNumber != null)
                {
                    TableNumber = int.Parse(Order.TableNumber);
                }
                
                IsRunning = false;
            }

            if (Products.Any())
            {
                IsProceedEnabled = true;
            }
            else
            {
                IsProceedEnabled = false;
            }
        }
        private void StepperValueChangedAsync()
        {
            LoadTotalPriceAsync();
        }
        private void LoadTotalPriceAsync()
        {
            if (Products != null)
            {
                Total = Products.Sum(x => x.Subtotal);
            }
            else
            {
                return;
            }
        }
        private async void SelectMenuAsync()
        {
            if (Order == null)
            {

                await _navigationService.NavigateAsync(nameof(EstablishmentPage));
            }
            else
            {
                Order.TableNumber = TableNumber.ToString();
                Order.OrderProducts = Products;
                Order.Total = (float)Total;
                var parameters = new NavigationParameters
                {
                    { Constants.customer, Customer},
                    { Constants.order, Order},
                    { Constants.collaborator, Collaborator},                    
                };
                await _navigationService.NavigateAsync(nameof(EstablishmentPage), parameters);
            }
        }
        private async void GoOpenCommand()
        {
            NewCommandAsync();
            //UpdateTrackingStatusAsync()
            await _navigationService.NavigateAsync($"/ProviderMasterDetailPage/NavigationPage/CommandTabbedPage?selectedTab=CommandPage");
        }
        private async void RemoveProductsAsync(OrderProduct product)
        {
            Total = 0;
            Products.Remove(product);
            Total = Products?.Sum(x => x.Subtotal);

            if (Products.Any())
            {
                IsProceedEnabled = true;
            }
            else
            {
                IsProceedEnabled = false;
            }

        }
        private async void NewCommandAsync()
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

            Order.TableNumber = TableNumber.ToString();
            Order.EstablishmentStaffId = Collaborator.Id;
            Order.OrderProducts = Products;
            Order.Total = (float)Total;
            Order.TrackingStatusId = 2;
            Orders request = Order;

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            PutResponse response = await _apiService.NewCommandAsync(url, Constants.servicePrefix, Constants.NewCommandAsyncController, Constants.tokenType, token.Result.token, request);

            IsRunning = false;
            IsEnabled = true;
            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.ResultMessages[0], "Aceptar");
                return;
            }

        }

        private async void UpdateTrackingStatusAsync()
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

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.UpdateTrackingStatus(url, Constants.servicePrefix, Constants.UpdateTrackingStatusController, Order.Id, 2, Constants.tokenType, token.Result.token);

            IsRunning = false;
            IsEnabled = true;

            if (response.ResultCode != ResultCode.Success)
            {
                await _dialogService.DisplayAlertAsync(Constants.titleError, response.ResultMessages[0], Constants.cancelButton);
                return;
            }
        }
    }
}
