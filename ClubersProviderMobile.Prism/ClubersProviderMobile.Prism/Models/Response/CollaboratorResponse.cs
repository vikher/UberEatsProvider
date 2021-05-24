using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class CollaboratorResponse : TransactionResult
    {
        public List<EstablishmentStaff> Result { get; set; }
    }
}
