using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Response : TransactionResult
    {
        //public bool IsSuccess { get; set; }

        //public string Message { get; set; }

        public object Result { get; set; }
    }
}
