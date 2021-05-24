using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Delivery : BaseDto<int>
    {
        public int DeliveryManId { get; set; }
        public virtual DeliveryMen DeliveryMan { get; set; }

        public string CustomerAddress { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
