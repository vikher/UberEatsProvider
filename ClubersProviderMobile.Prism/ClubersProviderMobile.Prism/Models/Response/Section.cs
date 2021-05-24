using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Section : BaseDto<int>
    {
        public string Name { get; set; }
        public bool Home { get; set; }
        public bool Onsite { get; set; }
        public TimeSpan StartHomeService { get; set; }
        public TimeSpan EndHomeService { get; set; }
        public TimeSpan StartOnsiteService { get; set; }
        public TimeSpan EndOnsiteService { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public string StartHomeServiceS { get; set; }
        public string EndHomeServiceS { get; set; }
        public string StartOnsiteServiceS { get; set; }
        public string EndOnsiteServiceS { get; set; }
    }
}
