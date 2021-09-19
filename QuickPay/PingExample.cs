using System;
using System.Threading.Tasks;
using RestSharp;

namespace Quickpay
{
	public class PingExample: QuickPayRestClient
	{
		public PingExample (string username, string password) : base (username, password)
		{
		}

		public PingExample (string apikey) : base (apikey)
		{
		}

		public PingResponse GetPing()
		{
			return CallEndpoint<PingResponse> ("ping");
		}

		public async Task<PingResponse> GetPingAsync()
		{
			return await CallEndpointAsync<PingResponse>("ping");
		}

		public PingResponse PostPing()
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				request.Method = Method.POST;
			}; 
			return CallEndpoint<PingResponse> ("ping", prepareRequest);
		}
	}
}

