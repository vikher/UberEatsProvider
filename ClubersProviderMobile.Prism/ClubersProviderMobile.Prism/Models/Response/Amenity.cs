using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Amenity : BaseDto<int>
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public int AmenityTypeId { get; set; }
        public virtual AmenityTypes AmenityTypes { get; set; }
    }
}
