using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class TipResponse : TransactionResult
    {
        public List<Tips> Result { get; set; }
    }

    public class Tips : BaseDto<int>
    {
        public float Amount { get; set; }
    }
}
