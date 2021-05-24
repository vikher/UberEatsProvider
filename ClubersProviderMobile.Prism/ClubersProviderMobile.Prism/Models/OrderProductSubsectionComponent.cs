using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class OrderProductSubsectionComponent : BaseDto<int>
    {
        public int OrderProductSubsectionId { get; set; }
        public int ComponentId { get; set; }
        [JsonIgnore]
        public float Price { get; set; }
    }
}
