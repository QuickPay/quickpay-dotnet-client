using System;
using System.Collections.Generic;

namespace Quickpay.Models
{
	public class AclPermission
	{
		public string resource { get; set; }
		public bool get { get; set; }
		public bool post { get; set; }
		public bool put { get; set; }
		public bool delete { get; set; }
		public bool patch { get; set; }
	}

	public class User
	{
		public int id { get; set; }
		public object email { get; set; }
		public bool system_user { get; set; }
		public string name { get; set; }
	}

	public class AgreementAccount
	{
		public int id { get; set; }
		public string type { get; set; }
		public string name { get; set; }
	}

	public class Agreement
	{
		public int id { get; set; }
		public bool owner { get; set; }
		public string api_key { get; set; }
		public string description { get; set; }
		public List<AclPermission> acl_permissions { get; set; }
		public bool accepted { get; set; }
		public bool locked { get; set; }
		public bool support { get; set; }
		public string service { get; set; }
		public User user { get; set; }
		public AgreementAccount account { get; set; }
		public string created_at { get; set; }
	}
}

