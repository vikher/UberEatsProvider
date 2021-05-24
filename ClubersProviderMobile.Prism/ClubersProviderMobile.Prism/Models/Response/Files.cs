using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Files : BaseDto<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Base64 { get; set; }
        public int? GaleryId { get; set; }
        public Galeries Galeries { get; set; }
    }
}
