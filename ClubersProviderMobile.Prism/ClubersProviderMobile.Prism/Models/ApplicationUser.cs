using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class ApplicationUser : BaseDto<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public int? GenderId { get; set; }
        public int? SquareId { get; set; }
        public int? CellId { get; set; }
        public int ZoneId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
