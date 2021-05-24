using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ProductSection : BaseDto<int>
    {
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}
