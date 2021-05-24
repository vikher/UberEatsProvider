using Xamarin.Forms;

namespace ClubersProviderMobile.Prism.Views.Command
{
    public partial class CashPaymentRegistrationPage : ContentPage
    {
        public CashPaymentRegistrationPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
