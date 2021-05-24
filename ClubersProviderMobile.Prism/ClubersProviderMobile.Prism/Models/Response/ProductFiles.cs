using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ProductFiles : BaseDto<int>
    {
        public int FileId { get; set; }
        public virtual Files Files { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}
