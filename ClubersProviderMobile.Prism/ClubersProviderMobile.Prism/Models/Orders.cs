using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Orders : BaseDto<int>
    {
        [JsonIgnore]
        public int EstablishmentId { get; set; }
        public int CustomerId { get; set; }
        //[JsonIgnore]
        public virtual Customers Customer { get; set; }
        [JsonIgnore]
        public int? RefundId { get; set; }
        [JsonIgnore]
        public int? BTransactionId { get; set; }

        public int PaymentId { get; set; }
        [JsonIgnore]
        public virtual Payment Payment { get; set; }

        public int ConsumptionTypeId { get; set; }
        [JsonIgnore]
        public virtual ConsumptionType ConsumptionType { get; set; }
        [JsonIgnore]
        public int? DeliveryId { get; set; }
        //[JsonIgnore]
        public virtual Delivery Delivery { get; set; }
        //[JsonIgnore]
        //public int? EvaluationId { get; set; }

        public DateTime StartDate { get; set; }

        [JsonIgnore]
        public DateTime? EndDate { get; set; }

        public int DeliveryFee { get; set; }

        public int? TipId { get; set; }
        public virtual Tips Tip { get; set; }

        public int TrackingStatusId { get; set; }

        public int? EstablishmentStaffId { get; set; }
        //[JsonIgnore]
        public virtual EstablishmentStaff EstablishmentStaff { get; set; }

        public float Total { get; set; }

        public string TableNumber { get; set; }

        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<RatingDetail> RatingDetails { get; set; }
        [JsonIgnore]
        public int QuantityProducts => OrderProducts.Count();

        [JsonIgnore]
        public int? NumberOfPackagesDelivered { get; set; }
        public float? AproxPreparationTime { get; set; }
        [JsonIgnore]
        public int TotalQuantityOfProducts { get => OrderProducts.Sum(q => q.Quantity); }
    }
}
