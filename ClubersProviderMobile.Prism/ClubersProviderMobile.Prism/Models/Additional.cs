using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Additional : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Product Product { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value, () => RaisePropertyChanged(nameof(Total)));
        }

        public int Total => (Quantity * Price);
    }
}
