using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ProductImage : BaseDto<int>
    {
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
        public string ImageURL { get; set; }
    }
}
