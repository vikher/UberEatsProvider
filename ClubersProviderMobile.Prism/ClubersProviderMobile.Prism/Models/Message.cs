using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        [JsonIgnore]
        public DateTime TimeReceived
        {
            get;
            set;
        }
    }
}
