using ClubersProviderMobile.Prism.Models;
using ClubersProviderMobile.Prism.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private bool _isProceedEnabled;
        private int _totalPrice;
        private Products _selectedProduct;
        private Orders _finalorder;
        private string _comments = "";
        private int _quantity;
        private float _onSiteTotal;
        private List<Ingredients> _ingredients;
        private List<Additional> _additional;
        private DelegateCommand _componentsCheckChangedCommand;
        private DelegateCommand _valueChangedCommand;
        private DelegateCommand _selectMenuCommand;
        public ProductDetailPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Detalle de producto";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
        }

        public DelegateCommand ValueChangedCommand => _valueChangedCommand ?? (_valueChangedCommand = new DelegateCommand(StepperValueChangedAsync));
        public DelegateCommand ComponentsCheckChangedCommand => _componentsCheckChangedCommand ?? (_componentsCheckChangedCommand = new DelegateCommand(ComponentsCheckedAsync));
        public DelegateCommand GoEstablishmentPageCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));
        #region Propiedades
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value, () => { RaisePropertyChanged(nameof(OnSiteTotal)); });
        }
        //public float OnSiteTotal => (Quantity * SelectedProduct.OnSitePrice);
        public float OnSiteTotal
        {
            get
            {
                if (SelectedProduct == null)
                {
                    _onSiteTotal = 0;
                }
                else
                {
                    _onSiteTotal = (Quantity * SelectedProduct.OnSitePrice);
                }
                
                return _onSiteTotal;
            }

            set => SetProperty(ref _onSiteTotal, value);
        }
        public bool IsProceedEnabled
        {
            get => _isProceedEnabled;
            set => SetProperty(ref _isProceedEnabled, value);
        }
        public string Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }
        public Products SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        public int TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }
        public Orders FinalOrder
        {
            get => _finalorder;
            set => SetProperty(ref _finalorder, value);
        }
        public List<Ingredients> Ingredients
        {
            get => _ingredients;
            set => SetProperty(ref _ingredients, value);
        }
        public List<Additional> Additionals
        {
            get => _additional;
            set => SetProperty(ref _additional, value);
        }

        #endregion Propiedades

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.product))
            {
                SelectedProduct = parameters.GetValue<Products>(Constants.product);
                Quantity = 1;
            }

            if (Ingredients == null)
            {
                IsProceedEnabled = true;
            }
        }
        private async void SelectMenuAsync()
        {
            IsRunning = true;
            if (SelectedProduct == null)
                return;
            var parameters = new NavigationParameters
            {
                { Constants.product, SelectedProduct},
                { "quantity", Quantity},
                { "comments", Comments}
            };
            await _navigationService.GoBackAsync(parameters);
            IsRunning = false;
        }
        private void StepperValueChangedAsync()
        {
            //Quantity += 1;
            //Quantity -= 1;
        }

        private void ComponentsCheckedAsync()
        {
            IsProceedEnabled = Ingredients.Any(x => x.Checked == true);
            for (int i = 0; i < Ingredients.Count; i++)
            {
                var item = Ingredients[i];

                if (item.Checked)
                {
                    if (item.Quantity == 0)
                    {
                        item.Quantity = 1;
                    }
                }
                else
                {
                    if (item.Quantity == 1)
                    {
                        item.Quantity = 0;
                    }
                }
            }
        }
    }
}
