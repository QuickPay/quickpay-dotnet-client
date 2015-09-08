using System;

namespace Quickpay
{
	[Obsolete ("This is an example class, please do not use in production")]
	public class AcquirerStatus
	{
		public string acquirer { get; set; }
		public string status { get; set; }
		public int health { get; set; }
	}
}

