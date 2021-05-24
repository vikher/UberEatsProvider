using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Order : BindableBase
    {
        public int Id { get; set; }

        public Tips Tip { get; set; }
        public string PaymentMethod { get; set; }
        public StatusName StatusName { get; set; } 
        public TrackingStatus TrackingStatus { get; set; }
        private int _quantity;
        public int QuantityProducts
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value, () => RaisePropertyChanged(nameof(Total)));
        }
        public double TotalToPay { get; set; }
        public decimal Total => QuantityProducts * TotalPrice;
        public decimal TotalPrice { get; set; }
        public string HourOrder { get; set; }
        public string TableNumber { get; set; }        
        public Customer Customer { get; set; }
        public EstablishmentStaff Collaborator { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public List<Product> Products { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
