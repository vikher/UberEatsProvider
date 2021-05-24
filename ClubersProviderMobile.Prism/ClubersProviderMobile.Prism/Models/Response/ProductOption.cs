using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ProductOption : BaseDto<int>
    {
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }
    }
}
