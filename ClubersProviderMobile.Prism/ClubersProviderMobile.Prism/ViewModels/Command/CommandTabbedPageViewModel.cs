using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class CommandTabbedPageViewModel : ViewModelBase
    {
        public CommandTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Comandas";
        }
    }
}
