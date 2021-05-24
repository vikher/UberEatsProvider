using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class SubsectionComponent : BaseDto<int>
    {
        public int ComponentId { get; set; }
        public Components Components { get; set; }
        public int SubsectionId { get; set; }
        public Subsection Subsection { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public decimal? Price { get; set; }
        public int? AvailabilitiesId { get; set; }
    }
}
