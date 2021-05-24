using System;
using System.Globalization;
using Xamarin.Forms;

namespace ClubersProviderMobile.Prism.Converters
{

    public class OrderStatusToStringValueConverter : IValueConverter
    {
        public string Domicilio = "A domicilio";
        public string EnSitio = "En sitio";
        public string Cancelado = "Cancelado";
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
            if (value.ToString() == "cancelled")
            {
                return Cancelado;
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
