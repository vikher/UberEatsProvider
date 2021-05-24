using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Department : BaseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
