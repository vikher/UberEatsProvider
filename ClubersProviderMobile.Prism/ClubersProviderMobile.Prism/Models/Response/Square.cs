using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Square : BaseDto<int>
    {
        public string SquareName { get; set; }
        public int LocationMapsId { get; set; }
        public virtual LocationMaps LocationMaps { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
        public int TotalZones { get; set; }
        public int TotalCells { get; set; }
        public string LatLng { get; set; }
    }
}
