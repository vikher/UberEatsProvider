using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class BussinnesLines : BaseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public int TotalCategories { get; set; }
        public bool Validado { get; set; }
        public string ValidatedName { get; set; }
    }
}
