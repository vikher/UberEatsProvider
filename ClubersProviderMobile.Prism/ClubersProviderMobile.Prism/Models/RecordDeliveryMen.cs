using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class RecordDeliveryMen : BaseDto<int>
    {
        public int TypeReviewId { get; set; }
        public int DeliveryMenId { get; set; }
        [ForeignKey("DeliveryMenId")]
        public virtual DeliveryMen DeliveryMen { get; set; }
        public int ConstumerId { get; set; }
        //[ForeignKey("ConstumerId")]
        //public virtual DeliveryMen DeliveryMen { get; set; } //ToDo agregar relacion entre consumidor a historial de reseñas
        public string Description { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public decimal Score { get; set; }
    }
}
