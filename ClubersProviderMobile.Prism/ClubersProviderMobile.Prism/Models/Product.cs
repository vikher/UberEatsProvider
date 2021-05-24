using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Product : BindableBase
    {

        public Order Order { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value, () => { RaisePropertyChanged(nameof(Total)); });
        }
        public TrackingStatus TrackingStatus { get; set; }
        public Availability Availability { get; set; }
        public Submenu Submenu { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public List<Additional> Additionals { get; set; }
        public string ImageUrl { get; set; }
        public int Total => ((Additionals?.Sum(x => x.Total)*Quantity) + (Quantity * Price)) ?? (Quantity * Price);
    }
}
