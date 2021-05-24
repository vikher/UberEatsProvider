using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class OrderProduct : BindableBase
    {
        [JsonIgnore]
        public int Id { get; set; }
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
        [JsonIgnore]
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        
        public Products? Product { get; set; }

        //public int Quantity { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value, () => { RaisePropertyChanged(nameof(Subtotal)); });
        }
        public float Subtotal => (Quantity * Product.OnSitePrice);
        //public float CalculatedOnSiteSubtotal => (Quantity * Product.OnSitePrice);

        public string Comments { get; set; }

        public virtual IEnumerable<OrderProductComponent> OrdersProductsComponents { get; set; }

        public virtual IEnumerable<OrderProductSubsection> OrdersProductsSubsections { get; set; }
        public string OrderDetail { get; set; }
    }
}
