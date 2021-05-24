using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Vehicle : BaseDto<int>
    {
        public int DeliveryMenId { get; set; }
        [ForeignKey("DeliveryMenId")]
        public virtual DeliveryMen DeliveryMen { get; set; }
        [Required]
        public int TypeVehicle { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string State { get; set; }

        //public int VehicleTypeId { get; set; }
        //public virtual VehicleType VehicleType { get; set; }
    }
}
