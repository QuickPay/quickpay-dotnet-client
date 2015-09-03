
using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using RestSharp;

namespace Quickpay.Util
{
	public static class StringExtensions
	{
		public static string ToSecret(this string apikey)
		{
			var tempkey = ":" + apikey;
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(tempkey));
		}
	}
}

