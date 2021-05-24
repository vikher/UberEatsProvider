using System.ComponentModel;

namespace ClubersProviderMobile.Prism
{
    public enum StatusName
    {
        [Description("En sitio")]
        onsite = 1,
        [Description("A domicilio")]
        home = 2,
        [Description("Cancelado")]
        cancelled = 3
    }
    public enum TrackingStatus
    {
        [Description("Nueva orden")] NewOrder = 1,
        [Description("En preparación")] InProcess = 2,
        [Description("Listo para entregar")] Ready = 3,
        [Description("En revisión")] Review = 4,
        [Description("En ruta de entrega")] InDeliveryProcess = 5,
        [Description("Entregado")] Delivered = 6,
        [Description("Cancelado")] Canceled = 7
    }
    public enum AppStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
    public enum ResultCode
    {
        Success = 1,
        Alert = 2,
        Warning = 3,
        Fatal = 4,
        Unauthorized = 5
    }
    public enum MailTemplateType
    {
        Register = 1,
        ResetPassword = 2,
        AccountConfirm = 3,
        PasswordChanged = 4,
    }
    public enum ProcessNotificationType
    {
        Progress = 1,
        Alert = 2,
    }
    public enum ProcessStatus
    {
        Executing = 1,
        Finished = 2
    }
    public enum RoleSystem
    {
        [Description("Administrador")] Administrator = 1,
        [Description("UsuarioPW")] WebPortalUser = 2,
        [Description("Socio Proveedor")] Provider = 3,
        [Description("Socio Consumidor")] Consumer = 4,
        [Description("Socio Repartidor")] DeliveryMan = 5,
        [Description("Administrador Shazam")] ShazamAdmin = 6
    }
    public enum MembershipValidity
    {
        [Description("Anual")] Annual = 1,
        [Description("Mensual")] Monthly = 2,
        [Description("Semestral")] Biannual = 3
    }
    public enum TrialPeriod
    {
        [Description("Un Mes")] OneMonth = 1,
        [Description("Dos Meses")] TwoMonth = 2,
        [Description("Sin Vigencia")] NoValidity = 3
    }
    public enum ClubersCashVilidity
    {
        [Description("6 Meses")] SixMonth = 1,
        [Description("2 Meses")] TwoMonth = 2,
        [Description("1 Mes")] OneMonth = 3
    }
    public enum RefundsValidity
    {
        [Description("30 días")] ThirtyDays = 1,
        [Description("15 días")] FifteenDays = 2,
        [Description("Sin vigencia")] NoValidity = 3
    }
    public enum ConsumptionTypes
    {
        [Description("Pedido a domicilio")] Delivery = 1,
        [Description("Recoger pedido")] Pickup = 2,
        [Description("Consumo en sitio")] OnSite = 3
    }
}
