using System;
using System.Collections.Generic;

namespace Quickpay
{
	[Obsolete ("This is an example class, please do not use in production")]
	public class Resource
	{
		public string name { get; set; }
		public int size { get; set; }
		public string mime { get; set; }
	}

	[Obsolete ("This is an example class, please do not use in production")]
	public class Branding
	{
		public int id { get; set; }
		public string name { get; set; }
		public int account_id { get; set; }
		public List<Resource> resources { get; set; }
		public string created_at { get; set; }
	}
}

