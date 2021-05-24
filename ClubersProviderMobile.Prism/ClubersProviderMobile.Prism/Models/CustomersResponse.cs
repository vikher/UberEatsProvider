
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class CustomersResponse : TransactionResult
    {
        public Customers Result { get; set; }
    }
}