using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Establishments : BaseDto<int>
    {
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public int BussinesLineId { get; set; }
        public virtual BussinnesLines BussinesLines { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }
        public int SubcategoryId { get; set; }
        public virtual Subcategories Subcategories { get; set; }
        public string RFC { get; set; }
        //public string UserName { get; set; }
        public string Email { get; set; }
        //public string PasswordHash { get; set; }
        public int PaymentDetailId { get; set; }
        public virtual PaymentDetail PaymentDetail { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int SquareId { get; set; }
        public virtual Square Square { get; set; }
        public int CellId { get; set; }
        public virtual Cell Cell { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public int? BusinessClassId { get; set; }
        public virtual BusinessClass BusinessClass { get; set; }
        public float EstablishmentPercent { get; set; }
        public float StaffPercent { get; set; }
        public string EstablishmentLogoWEB { get; set; }
        public int EstablishmentHoursId { get; set; }
        public bool Receive { get; set; }
        public bool Validado { get; set; }
        public virtual EstablishmentHours EstablishmentHours { get; set; }
        public virtual List<EstablismentRefund> EstablismentRefunds { get; set; }
        public virtual List<EstablismentSocialMedia> EstablismentSocialMedia { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<EstablishmentFiles> EstablishmentFiles { get; set; }
        public virtual List<EstablishmentStaff> EstablishmentStaffs { get; set; }
        public virtual List<EstablishmentAmenities> EstablishmentAmenities { get; set; }
        public virtual List<EstablishmentGalery> EstablishmentGaleries { get; set; }
        public string BussinesLineName { get; set; }
        public string Manager { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
    }
}
