using ClubersProviderMobile.Prism;
using System;
using System.Globalization;
using Xamarin.Forms;


namespace ClubersCustomerMobile.Prism.Converters
{
    public class OrderTypeValueConverter : IValueConverter
    {
        public string Domicilio = Constants.icons + "llevar.png";
        public string EnSitio = Constants.icons + "ensitio.png";
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            if (value.ToString() == "1")
            {
                return Domicilio;
            }
            if (value.ToString() == "3")
            {
                return EnSitio;
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
