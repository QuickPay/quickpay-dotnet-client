using RestSharp;
using System;
using QuickPay.Models.Ping;

namespace QuickPay.Services
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
