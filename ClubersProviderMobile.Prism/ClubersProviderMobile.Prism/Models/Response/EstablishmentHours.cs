using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablishmentHours : BaseDto<int>
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Onsite { get; set; }
        public bool Home { get; set; }
        public bool Exclusive { get; set; }
        public TimeSpan StartHomeService { get; set; }
        public TimeSpan EndHomeService { get; set; }
        public TimeSpan StartOnsiteService { get; set; }
        public TimeSpan EndOnsiteService { get; set; }
        public string StartHomeServiceS { get; set; }
        public string EndHomeServiceS { get; set; }
        public string StartOnsiteServiceS { get; set; }
        public string EndOnsiteServiceS { get; set; }
    }
}
