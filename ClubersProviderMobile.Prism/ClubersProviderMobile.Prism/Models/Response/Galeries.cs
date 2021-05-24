using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Galeries : BaseDto<int>
    {
        public string Name { get; set; }

        public virtual IEnumerable<GaleryImage> GaleryImages { get; set; }
    }
}
