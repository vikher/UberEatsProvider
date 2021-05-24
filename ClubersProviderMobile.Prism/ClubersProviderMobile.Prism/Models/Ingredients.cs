using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public bool Checked { get; set; }
    }
}
