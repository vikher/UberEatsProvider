using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        private bool _isRunning;
        private bool _isEnabled;

        private string _title;
        private string _errorPlaceholder = Constants.icons + "ErrorPlaceholder.png";
        private string _deleteIcon = Constants.icons + "delete.png";
        private string _waiterIcon = Constants.icons + "clubers_SP_mesero.png";
        private string _delivery = Constants.icons + "clubers_SP_repartidor.png";
        private string _reader = Constants.icons + "clubers_SP_lector.png";
        private string _ine = Constants.icons + "clubers_SP_INE.png";
        private string _waytopay = Constants.icons + "clubers_SP_ico_formadepago.png";
        private string _onsite = Constants.icons + "ensitio.png";
        private string _paidout = Constants.icons + "clubers_SP_ico_pagado.png";
        private string _establishment = Constants.icons + "clubers_SP_ico_establecimiento.png";
        private string _mail = Constants.icons + "clubers_SP_ico_mail.png";
        private string _telephone = Constants.icons + "clubers_SP_ico_tel.png";
        private string _direction = Constants.icons + "clubers_SP_ico_direccion.png";
        private string _homeService = Constants.icons + "clubers_SP_ico_serviciositio.png";
        private string _onSiteService = Constants.icons + "clubers_SP_ico_serviciodimicilio.png";
        private bool _isRefreshing;        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public string ErrorPlaceholder
        {
            get { return _errorPlaceholder; }
            set { SetProperty(ref _errorPlaceholder, value); }
        }
        public string DeleteIcon
        {
            get { return _deleteIcon; }
            set { SetProperty(ref _deleteIcon, value); }
        }
        public string WaiterIcon
        {
            get { return _waiterIcon; }
            set { SetProperty(ref _waiterIcon, value); }
        }
        public string Delivery
        {
            get { return _delivery; }
            set { SetProperty(ref _delivery, value); }
        }
        public string Reader
        {
            get { return _reader; }
            set { SetProperty(ref _reader, value); }
        }
        public string INE
        {
            get { return _ine; }
            set { SetProperty(ref _ine, value); }
        }
        public string WayToPay
        {
            get { return _waytopay; }
            set { SetProperty(ref _waytopay, value); }
        }
        public string OnSite
        {
            get { return _onsite; }
            set { SetProperty(ref _onsite, value); }
        }
        public string PaidOut
        {
            get { return _paidout; }
            set { SetProperty(ref _paidout, value); }
        }
        public string Establishment
        {
            get { return _establishment; }
            set { SetProperty(ref _establishment, value); }
        }
        public string Mail
        {
            get { return _mail; }
            set { SetProperty(ref _mail, value); }
        }
        public string Telephone
        {
            get { return _telephone; }
            set { SetProperty(ref _telephone, value); }
        }
        public string Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        public string HomeService
        {
            get { return _homeService; }
            set { SetProperty(ref _homeService, value); }
        }
        public string OnSiteService
        {
            get { return _onSiteService; }
            set { SetProperty(ref _onSiteService, value); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
