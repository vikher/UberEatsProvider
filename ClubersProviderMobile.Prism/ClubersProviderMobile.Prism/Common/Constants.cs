using Xamarin.Forms;

namespace ClubersProviderMobile.Prism
{
    public static class Constants
    {
        public static string HostName { get; set; } = "https://chatserverclubers.azurewebsites.net";
        public static string MessageName { get; set; } = "newMessage";
        public static string Username
        {
            get
            {
                return $"{Device.RuntimePlatform} User";
            }
        }

        public const string GoogleMapsApiKey = "AIzaSyCQuwuhH4ACaaLBOpC-FEQvMSuKEnh86AE";
        public const string urlBase = "http://clubers.qagperti.tk/";
        public const string servicePrefix = "api";
        public const string tokenType = "bearer";
        public const string accessToken = "";
        public const string controller = "";
        //Controllers
        public const string PutCommandAsyncController = "/Order/PutCommandAsync";
        public const string GetUserByEmailAsyncController = "/Account/GetUserByEmailAsync";
        public const string GetCustomerByINEAsyncController = "/Customer/GetCustomerByINEAsync";
        public const string GetCustomerByIdAsyncController = "/Customer/GetCustomerByIdAsync";
        public const string GetAllProductsByEstablishmentIDAsyncController = "/Product/GetAllProductsByEstablishmentIDAsync";
        public const string UpdateProductAvailableController = "/Product/UpdateProductAvailable";
        public const string GetCollaboratorsDetalisByEstablishmentIDAsyncController = "/EstablishmentStaff/GetCollaboratorsDetalisByEstablishmentIDAsync";
        public const string GetEstablishmentByIdAsyncController = "/Establishment/GetEstablishmentByIdAsync";
        public const string GetTipsByEstablishmentIDAsyncController = "/Establishment/GetTipsByEstablishmentIDAsync";
        public const string UpdateEstablishmentReceiveController = "/Establishment/UpdateEstablishmentReceive";
        public const string GetAllCollaboratorByEstablishmentIDAsyncController = "/Establishment/GetAllCollaboratorByEstablishmentIDAsync";
        public const string NewCommandAsyncController =  "/Order/NewCommandAsync";
        public const string UpdateTrackingStatusController = "/order/UpdateTrackingStatus";
        public const string GetAllOrdersByEstablishmentIdAsyncController =  "/Order/GetAllOrdersByEstablishmentIdAsync";
        public const string PutNumberOfPackagesDeliveredAsyncController =  "/Order/PutNumberOfPackagesDeliveredAsync";

        //Whatsapp
        public const string wa1 = "https://wa.me/+522381995195?text=Tengo%20un%20problema%20con%20un%20pedido";
        public const string wa2 = "https://wa.me/+522381995195?text=Tengo%20un%20problema%20con%20un%20Socio%20Consumidor";
        public const string wa3 = "https://wa.me/+522381995195?text=Tengo%20un%20problema%20con%20un%20cobro";
        public const string wa4 = "https://wa.me/+522381995195?text=Tengo%20un%20problema%20con%20un%20Saldo";

        //DisplayAlertAsync
        public const string titleError = "Error";
        public const string titleOk = "Ok.";
        public const string cancelButton = "Aceptar";
        public const string connectionError = "Compruebe la conexión a Internet";
        public const string idValid = "Debes ingresar un id válido";
        public const string unregisteredUser = "Usuario no registrado";
        public const string enterEmail = "Debe ingresar un correo.";
        public const string enterPassword = "Debe ingresar una contraseña.";
        public const string charactersPassword = "La contraseña debe contener al menos 8 caracteres.";
        public const string confirmPassword = "Debe confirmar la contraseña.";
        public const string enterConfirmPassword = "La contraseña y la confirmación no coinciden.";
        public const string updateSuccess = "Contraseña actualizada con éxito";

        //NavigationParameters
        public const string customer = "customer";
        public const string order = "order";
        public const string collaborator = "collaborator";
        public const string products = "products";
        public const string product = "product";
        public const string tip = "tip";

        //Url Icons 
        public const string icons = "http://clubers.qagperti.tk/Resources/icons/";
    }
}

