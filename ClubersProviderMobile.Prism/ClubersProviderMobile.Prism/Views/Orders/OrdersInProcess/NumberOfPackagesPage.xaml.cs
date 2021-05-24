using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ClubersProviderMobile.Prism.Views
{
    public partial class NumberOfPackagesPage : PopupPage
    {
        public NumberOfPackagesPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        /*protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (Navigation.NavigationStack.Count > 0)
            {
                var currentPage = Navigation.NavigationStack[0];
                var pageList = Navigation.NavigationStack.Where(y => y != currentPage).ToList();

                foreach (var page in pageList)
                    Navigation.RemovePage(page);
            } else
            {
                var currentPage = Navigation.NavigationStack[0];
                var pageList = Navigation.NavigationStack.Where(y => y != currentPage).ToList();

                foreach (var page in pageList)
                    Navigation.RemovePage(page);

            }
        }*/
    }
}
