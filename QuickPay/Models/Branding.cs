using System;
using System.Collections.Generic;

namespace Quickpay
{
	public class Resource
	{
		public string name { get; set; }
		public int size { get; set; }
		public string mime { get; set; }
	}

	public class Branding
	{
		public int id { get; set; }
		public string name { get; set; }
		public int account_id { get; set; }
		public List<Resource> resources { get; set; }
		public string created_at { get; set; }
	}
}

