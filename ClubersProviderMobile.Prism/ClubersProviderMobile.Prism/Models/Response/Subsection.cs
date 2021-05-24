using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Subsection : BaseDto<int>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public short ComponentsMax { get; set; }
        public bool Required { get; set; }
        public int TotalComponents { get; set; }
        public string StatusName { get; set; }
        public virtual List<SubsectionComponent> Components { get; set; }
    }
}
