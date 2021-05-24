using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class OrderProductComponent : BaseDto<int>
    {
        public int OrderProductId { get; set; }

        public int ComponentId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }
    }
}
