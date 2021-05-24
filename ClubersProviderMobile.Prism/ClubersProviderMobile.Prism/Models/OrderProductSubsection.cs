using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClubersProviderMobile.Prism.Models
{
    public class OrderProductSubsection : BaseDto<int>
    {
        [JsonIgnore]
        public int OrderProductId { get; set; }

        public int SubsectionId { get; set; }

        public virtual IEnumerable<OrderProductSubsectionComponent> OrdersProductsSubsectionsComponents { get; set; }
    }
}
