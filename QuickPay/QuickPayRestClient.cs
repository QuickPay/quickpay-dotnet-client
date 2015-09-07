using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using RestSharp;
using Quickpay.Util;
using Quickpay.Models;

namespace Quickpay
{
	public class QuickPayRestClient
	{
		public RestClient Client { get; set; }

		public const string BASE_URL = "https://api.quickpay.net/";

		public QuickPayRestClient (string username, string password)
		{
			Client = new RestClient (BASE_URL) {
				Authenticator = new HttpBasicAuthenticator (username, password),
				UserAgent = "QuickPay .Net client"
			};
		}

		public QuickPayRestClient (string apikey)
		{
			Client = new RestClient (BASE_URL) {
				Authenticator = new HttpBasicAuthenticator (string.Empty, apikey)
			};
		}

		private RestRequest CreateRequest (string resource)
		{
			var request = new RestRequest (resource);
			request.AddHeader ("Accept-Version", "v10");
			return request;
		}

		public PingResponse Ping ()
		{
			var request = CreateRequest ("ping");

			var response = Client.Execute<PingResponse> (request);
			VerifyResponse (response);
			return response.Data;
		}

		public async Task<List<AclResource>> AclResourcesAsync (AccountType accountType = AccountType.Any, int page = 1,
		                                                       int pageSize = 20)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				request.AddParameter ("page", page);
				request.AddParameter ("page_size", pageSize);
				if (accountType != AccountType.Any)
					request.AddParameter ("account_type", accountType.GetName ());
			}; 
			return CallEndpoint<List<AclResource>> ("acl-resources", prepareRequest);
		}

		public List<Payment> Payments ()
		{
			return CallEndpoint<List<Payment>> ("payments");
		}

		public List<Activity> Activity ()
		{
			return CallEndpoint<List<Activity>> ("activity");
		}

		public List<AcquirerStatus> AcquirerOperationalStatus ()
		{
			return CallEndpoint<List<AcquirerStatus>> ("operational-status/acquirers");
		}


		private T CallEndpoint<T> (string endpointName, Action<RestRequest> prepareRequest = null) where T: new()
		{
			var request = CreateRequest (endpointName);
			if (prepareRequest != null)
				prepareRequest (request);

			var response = Client.Execute<T> (request);

			VerifyResponse (response);
			return response.Data;	
		}

		private List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode> () {
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.Accepted
		};

		private void VerifyResponse<T> (IRestResponse<T> response)
		{
			if (response.StatusCode == HttpStatusCode.NotFound) {
				throw new Exception ("Endpoint not found, please note this could mean you are not authorized to access this endpoint");
			}
			if (!OkStatusCodes.Contains (response.StatusCode)) {
				throw new Exception (response.StatusDescription);
			}
		}
	}
}
