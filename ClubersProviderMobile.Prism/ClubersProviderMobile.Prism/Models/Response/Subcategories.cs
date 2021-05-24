using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Subcategories : BaseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Base64 { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public Types Types { get; set; }
        public Categories Categories { get; set; }
        public string TypeName { get; set; }
        public string CategoryName { get; set; }
        public string BussinnesLineName { get; set; }
        public bool Validado { get; set; }
        public string ValidatedName { get; set; }
    }
}
