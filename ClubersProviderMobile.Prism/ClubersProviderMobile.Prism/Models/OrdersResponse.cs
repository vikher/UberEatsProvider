using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
	public class OrdersResponse : TransactionResult
	{
		public List<Orders> Result { get; set; }
	}
}
