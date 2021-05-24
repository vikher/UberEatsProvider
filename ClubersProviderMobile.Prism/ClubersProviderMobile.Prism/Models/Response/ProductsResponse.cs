using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    class ProductsResponse : TransactionResult
    {
        public List<Products> Result { get; set; }
    }
}
