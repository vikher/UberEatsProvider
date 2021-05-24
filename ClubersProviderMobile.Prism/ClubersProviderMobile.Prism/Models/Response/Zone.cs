using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Zone : BaseDto<int>
    {
        public string ZoneName { get; set; }
        public int LocationMapsId { get; set; }
        public virtual LocationMaps LocationMaps { get; set; }
        public int? SquareId { get; set; }
        public virtual Square Square { get; set; }
        public virtual ICollection<Cell> Cells { get; set; }
        public int TotalCells { get; set; }
        public string LatLng { get; set; }
        public string SquareName { get; set; }
    }
}
