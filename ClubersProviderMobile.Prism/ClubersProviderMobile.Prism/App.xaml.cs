using Prism;
using Prism.Ioc;
using ClubersProviderMobile.Prism.ViewModels;
using ClubersProviderMobile.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClubersProviderMobile.Prism.Helpers;
using ClubersProviderMobile.Prism.Views.Orders;
using ClubersProviderMobile.Prism.Services;
using ClubersProviderMobile.Prism.Views.Orders.OrdersInProcess;
using ClubersProviderMobile.Prism.Views.Orders.ReadyOrders;
using ClubersProviderMobile.Prism.Views.Command;
using ClubersProviderMobile.Prism.Views.Login;
using ClubersProviderMobile.Prism.Interfaces;
using ClubersProviderMobile.Prism.Views.Command.OpenCommand;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ClubersProviderMobile.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Device.SetFlags(new string[] {"Expander_Experimental",
                "SwipeView_Experimental"
            });
            await NavigationService.NavigateAsync("/NavigationPage/LoginPage");

            if (Settings.IsRemembered && Settings.IsLogin)
            {
                await NavigationService.NavigateAsync("/ProviderMasterDetailPage/NavigationPage/OrdersTabbedPage?selectedTab=NewOrdersPage");
                
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            //Services
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            //Pages
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProviderMasterDetailPage, ProviderMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<OrdersTabbedPage, OrdersTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<NewOrdersPage, NewOrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<OrdersInProcessPage, OrdersInProcessPageViewModel>();
            containerRegistry.RegisterForNavigation<ReadyOrdersPage, ReadyOrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<HomeOrdersPage, HomeOrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoricOrdersTabbedPage, HistoricOrdersTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<OnSiteOrdersPage, OnSiteOrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<CanceledOrdersPage, CanceledOrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<SupportPage, SupportPageViewModel>();
            containerRegistry.RegisterForNavigation<HomeOrderDetailPage, HomeOrderDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<OrderDetailPage, OrderDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<OrdersInProcessDetailPage, OrdersInProcessDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ReadyOrdersDetailsPage, ReadyOrdersDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuAvailabilityPage, MenuAvailabilityPageViewModel>();
            containerRegistry.RegisterForNavigation<BalanceInquiryPage, BalanceInquiryPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchWaiterPage, SearchWaiterPageViewModel>();
            containerRegistry.RegisterForNavigation<OnSiteOrderDetailPage, OnSiteOrderDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CommandPage, CommandPageViewModel>();
            containerRegistry.RegisterForNavigation<ScanQrPage, ScanQrPageViewModel>();
            containerRegistry.RegisterForNavigation<IdCustomerPage, IdCustomerPageViewModel>();
            containerRegistry.RegisterForNavigation<EstablishmentPage, EstablishmentPageViewModel>();
            containerRegistry.RegisterForNavigation<CommandDetailPage, CommandDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CommandTabbedPage, CommandTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<NewCommandPage, NewCommandPageViewModel>();
            containerRegistry.RegisterForNavigation<ReviewCommandPage, ReviewCommandPageViewModel>();
            containerRegistry.RegisterForNavigation<ApproveDishPage, ApproveDishPageViewModel>();
            containerRegistry.RegisterForNavigation<OpenCommandPage, OpenCommandPageViewModel>();
            containerRegistry.RegisterForNavigation<CollaboratorPage, CollaboratorPageViewModel>();
            containerRegistry.RegisterForNavigation<CollaboratorAddedPage, CollaboratorAddedPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailPage, ProductDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<WaiterTipsDetailPage, WaiterTipsDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<PurchaseDetailPage, PurchaseDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CommandByCustomer, CommandByCustomerViewModel>();
            containerRegistry.RegisterForNavigation<QrCodeReaderPage, QrCodeReaderPageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePasswordPage, UpdatePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePasswordPage, UpdatePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<CancelledDeliveryDetailPage, CancelledDeliveryDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CancelledCollaboratorDetailPage, CancelledCollaboratorDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ApproveDishDetailPage, ApproveDishDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CashPaymentPage, CashPaymentPageViewModel>();
            containerRegistry.RegisterForNavigation<AddTipPage, AddTipPageViewModel>();
            containerRegistry.RegisterForNavigation<AddedTipPage, AddedTipPageViewModel>();
            containerRegistry.RegisterForNavigation<CashPaymentRegistrationPage, CashPaymentRegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentAppPage, PaymentAppPageViewModel>();
            containerRegistry.RegisterForNavigation<NumberOfPackagesPage, NumberOfPackagesPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomTipPage, CustomTipPageViewModel>();
            containerRegistry.RegisterForNavigation<MessagesPage, MessagesPageViewModel>();
            containerRegistry.RegisterForNavigation<ReviewCommandDetailPage, ReviewCommandDetailPageViewModel>();

            containerRegistry.RegisterForNavigation<EstablishmentReviewPage, EstablishmentReviewPageViewModel>();
        }
    }
}
