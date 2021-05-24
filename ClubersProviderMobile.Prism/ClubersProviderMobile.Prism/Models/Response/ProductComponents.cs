using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ProductComponents : BaseDto<int>
    {
        public int ComponentId { get; set; }
        public Components Components { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public decimal? Price { get; set; }
    }
}
