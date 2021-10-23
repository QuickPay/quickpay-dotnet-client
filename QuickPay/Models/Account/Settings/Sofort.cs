namespace Quickpay.Models.Account.Settings
{
	public class Sofort
	{
		public bool active { get; set; }
		public int customer_number { get; set; }
		public int project_id { get; set; }
		public object api_key { get; set; }
		public bool gateway { get; set; }
		public bool ideal { get; set; }
		public int ideal_project_id { get; set; }
		public string ideal_project_password { get; set; }
		public string ideal_notification_password { get; set; }
	}
}