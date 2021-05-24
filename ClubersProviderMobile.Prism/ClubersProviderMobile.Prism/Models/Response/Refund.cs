using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Refund : BaseDto<int>
    {
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public float RefundPercent { get; set; }
        public int RefundsValidity { get; set; }
        public string RefundsValidityText { get; set; }
        public List<ItemList> RefundsValidityOptions { get; set; } = new List<ItemList>()
        {
            new ItemList{ Value=1,Key="30 días"},
            new ItemList{ Value=2,Key="15 días"},
            new ItemList{ Value=3,Key="Sin vigencia"}
        };
        public decimal LimitOfTickets { get; set; }
        public bool InPlace { get; set; } = false;
        public bool HomeService { get; set; } = false;
        public bool MembershipRenewal { get; set; } = false;
        public float ClubersGainPercent { get; set; }
        public float SPGainPercent { get; set; }
        public int VisitNumber { get; set; }
        public int VisitNumberStart { get; set; }
        public float HighgerConsum { get; set; }
        public string Prize { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool ConfigProvider { get; set; }
        //public virtual ICollection<MembershipRefunds> MembershipRefunds { get; set; }
    }
    public class ItemList
    {
        public string Key { get; set; }
        public int Value { get; set; }

    }
}
