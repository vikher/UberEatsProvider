using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Collections.ObjectModel;
using ClubersProviderMobile.Prism.Models;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ProviderMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ProviderMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {

                new Menu
                {
                    Icon = IconFont.ClipboardAlertOutline ,
                    PageName = "OrdersTabbedPage?selectedTab=NewOrdersPage",
                    Title = "Mis Pedidos"
                },

                new Menu
                {
                    Icon = IconFont.Table,
                    PageName = "CommandTabbedPage?selectedTab=NewCommandPage",
                    Title = "Comandas"
                },

                new Menu
                {
                    Icon = IconFont.ClipboardCheck,
                    PageName = "HistoricOrdersTabbedPage?selectedTab=HomeOrdersPage",
                    Title = "Historial de pedidos"
                },

                new Menu
                {
                    Icon = IconFont.Cash ,
                    PageName = "SearchWaiterPage",
                    Title = "Consulta de saldos"
                },
                new Menu
                {
                    Icon = IconFont.Food,
                    PageName = "MenuAvailabilityPage",
                    Title = "Disponibilidad de menú"
                },
                new Menu
                {
                    Icon = IconFont.AccountSettings ,
                    PageName = "SettingsPage",
                    Title = "Configuración"
                },                
                new Menu
                {
                    Icon = IconFont.CommentQuestion ,
                    PageName = "SupportPage",
                    Title = "Ayuda"
                },
                new Menu
                {
                    Icon = IconFont.Logout,
                    PageName = "LoginPage",
                    Title = "Cerrar sesión"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
