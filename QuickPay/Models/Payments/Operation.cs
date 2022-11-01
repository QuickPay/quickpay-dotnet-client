using System;

namespace Quickpay.Models.Payments
{
	public class Operation
	{
		public int id { get; set; }
		public string type { get; set; }
		public int amount { get; set; }
		public bool pending { get; set; }
		public string qp_status_code { get; set; }
		public string qp_status_msg { get; set; }
		public string aq_status_code { get; set; }
		public string aq_status_msg { get; set; }
		public object data { get; set; }
		public string callback_url { get; set; }
		public Boolean? callback_success { get; set; }
		public string callback_response_code { get; set; }
		public string callback_duration { get; set; }
		public string acquirer { get; set; }
		//public string 3d_secure_status { get; set; }
		public string callback_at { get; set; }
		public string created_at { get; set; }
	}
}