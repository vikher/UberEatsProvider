using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class DeliveryMen : BaseDto<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Rfc { get; set; }
        public int? IdFrontImage { get; set; }
        public int? IdBackImage { get; set; }
        public int? IdPerfilImage { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string LatLong { get; set; }
        [Required]
        public int IdBackpackType { get; set; }
        [Required]
        public int IdGender { get; set; }
        [Required]
        public int IdSquare { get; set; }

        //public int? SRBalanceId { get; set; }
        //public virtual SRBalance SRBalance { get; set; }

        public int? BagId { get; set; }
        // public virtual Bag Bag { get; set; }
        public int? UserId { get; set; }
        public virtual UserDTO applicationUser { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<RecordDeliveryMen> RecordDeliveryMens { get; set; }
        // public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
