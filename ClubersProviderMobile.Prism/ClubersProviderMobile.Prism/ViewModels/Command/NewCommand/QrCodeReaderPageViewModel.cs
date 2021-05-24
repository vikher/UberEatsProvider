using ClubersProviderMobile.Prism.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ClubersProviderMobile.Prism.ViewModels
{
    public class QrCodeReaderPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;

        private bool _isAnalyzing = true;
        private bool _isScanning = true;
        private bool _flashButtonVisible;
        private string _topText = "Text";
        private string _bottomText = "Text";
        ZXingScannerView _zxing = new ZXingScannerView();

        public string TopText
        {
            get { return _topText; }
            set { SetProperty(ref _topText, value); }
        }
        public string BottomText
        {
            get { return _bottomText; }
            set { SetProperty(ref _bottomText, value); }
        }
        public bool ShowFlashButton
        {
            get { return _flashButtonVisible; }
            set
            {
                if (!bool.Equals(_flashButtonVisible, value))
                {
                    this._flashButtonVisible = value;
                    SetProperty(ref _flashButtonVisible, value);
                }
            }
        }
        public ZXing.Result Result { get; set; }
        public bool IsAnalyzing
        {
            get { return this._isAnalyzing; }
            set
            {
                if (!bool.Equals(_isAnalyzing, value))
                {
                    _isAnalyzing = value;
                    SetProperty(ref _isAnalyzing, value);
                }
            }
        }
        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                if (!bool.Equals(_isScanning, value))
                {
                    this._isScanning = value;
                    SetProperty(ref _isScanning, value);
                }
            }
        }
        public Command QRScanResultCommand => new Command(() =>
        {
            IsAnalyzing = false;
            IsScanning = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsRunning = true;
                try
                {
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    IsAnalyzing = false;

                    // do something with Result.Text
                    var parameters = new NavigationParameters
                    {
                        { "Id", Result.Text }
                    };
                    await _navigationService.GoBackAsync(parameters);
                }
                catch (Exception ex)
                {
                    IsRunning = false;
                    await _dialogService.DisplayAlertAsync(Constants.titleError, ex.Message, Constants.cancelButton);
                }
                IsRunning = false;
            });
        });
        public QrCodeReaderPageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Lector de Código QR";
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            ShowFlashButton = true;
        }
    }
}
