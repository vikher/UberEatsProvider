using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Categories : BaseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Base64 { get; set; }
        public int BussinesLineId { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public BussinnesLines BussinesLines { get; set; }
        public string BussinesLineName { get; set; }
        public virtual ICollection<Subcategories> Subcategories { get; set; }
        public int TotalSubcategories { get; set; }
        public bool Validado { get; set; }
        public string ValidatedName { get; set; }
    }
}
