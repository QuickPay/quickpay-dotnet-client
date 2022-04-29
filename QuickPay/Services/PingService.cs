using Quickpay.Models.Ping;
using RestSharp;
using System;

namespace Quickpay.Services
{
    public class PingService : QuickPayRestClient
    {
		public PingService(string username, string password) : base(username, password)
		{
		}

		public PingService(string apikey) : base(apikey)
		{
		}

		public Pong ping()
        {
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				request.Method = Method.Get;
			};

			return CallEndpointAsync<Pong>("ping", prepareRequest).GetAwaiter().GetResult();
		}
    }
}
