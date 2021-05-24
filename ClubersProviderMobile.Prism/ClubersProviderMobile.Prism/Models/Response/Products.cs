using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClubersProviderMobile.Prism.Models
{
    public class Products : BaseDto<int>
    {
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float HomePrice { get; set; }
        public float OnSitePrice { get; set; }
        public bool AvailableHome { get; set; }
        public bool AvailableOnSite { get; set; }
        public short PreparationTime { get; set; }
        public bool Available { get; set; }
        public string Base64 { get; set; }
        [JsonIgnore]
        public List<int> ProductSectionIds { get; set; } = new List<int>();
        [JsonIgnore]
        public List<int> ComponentsPrimaryIds { get; set; } = new List<int>();
        [JsonIgnore]
        public List<int> ComponentsSecondaryIds { get; set; } = new List<int>();
        [JsonIgnore]
        public List<int> ComponentsExtrasIds { get; set; } = new List<int>();
        public List<Components> ComponentsExtras { get; set; } = new List<Components>();
        public ICollection<ProductFiles> ProductFiles { get; set; }
        public ICollection<ProductSection> ProductSection { get; set; }
        public ICollection<ProductComponents> ProductComponents { get; set; }
        public ICollection<Subsection> Subsections { get; set; }
        public ICollection<ProductOption> ProductOptions { get; set; }
    }
}
