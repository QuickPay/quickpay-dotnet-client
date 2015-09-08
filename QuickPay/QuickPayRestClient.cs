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
	// TODO make async endpoints 

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
			request.AddHeader("accept", "application/json, text/plain, */*");
			return request;
		}

		public PingResponse Ping ()
		{
			return CallEndpoint<PingResponse> ("ping");
		}

		public Account Account()
		{
			return CallEndpoint<Account> ("account");
		}

		public List<AclResource> AclResources (AccountType accountType = AccountType.Any, PageParameters? pageParameters = null)
		{
			if (pageParameters == null)
				pageParameters = new PageParameters ();

			Action<RestRequest> prepareRequest = (RestRequest request) => {
				request.AddParameter ("page", pageParameters.Value.Page);
				request.AddParameter ("page_size", pageParameters.Value.PageSize);
				if (accountType != AccountType.Any)
					request.AddParameter ("account_type", accountType.GetName ());
			}; 

			return CallEndpoint<List<AclResource>> ("acl-resources", prepareRequest);
		}

		public List<Agreement> Agreements()
		{
			return CallEndpoint<List<Agreement>> ("agreements");
		}

		public List<Payment> Payments ()
		{
			return CallEndpoint<List<Payment>> ("payments");
		}

		public List<Branding> Branding()
		{
			return CallEndpoint<List<Branding>> ("brandings");
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

	public struct PageParameters
	{
		public PageParameters () : this(1, 20){}

		public PageParameters (int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}
		public int Page { get; set;}
		public int PageSize { get; set;}
	}
}
