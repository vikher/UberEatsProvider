using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
        [JsonIgnore]
        public DateTime Created { get; set; }
        [JsonIgnore]
        public int CreatedById { get; set; }
        [JsonIgnore]
        public DateTime? Modified { get; set; }
        [JsonIgnore]
        public int? ModifiedById { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
    }
}
