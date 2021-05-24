using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StatusTypeId { get; set; }
    }
}
