using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Components : BaseDto<int>
    {
        public string Name { get; set; }
        public int ComponentTypeId { get; set; }
        public decimal? Price { get; set; }
    }
}
