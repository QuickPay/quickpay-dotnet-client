using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using Quickpay.Util;
using Quickpay.Models;

namespace Quickpay
{
	// TODO make async endpoints
	// TODO add to nuget

	public class QuickPayRestClient
	{
		protected RestClient Client { get; set; }

		public const string BASE_URL = "https://api.quickpay.net/";

		public QuickPayRestClient (string username, string password)
		{
			Client = new RestClient (BASE_URL) {
				Authenticator = new HttpBasicAuthenticator (username, password),
				UserAgent = "QuickPay .Net client"
			};
		}

		public QuickPayRestClient (string apikey) : this(string.Empty, apikey)
		{		}

		public PingResponse Ping ()
		{
			return CallEndpoint<PingResponse> ("ping");
		}

		public Account Account ()
		{
			return CallEndpoint<Account> ("account");
		}

		public List<AclResource> AclResources (AccountType accountType = AccountType.Any, PageParameters? pageParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				if (accountType != AccountType.Any)
					request.AddParameter ("account_type", accountType.GetName ());
			}; 

			return CallEndpoint<List<AclResource>> ("acl-resources", prepareRequest);
		}

		public List<Agreement> Agreements (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			}; 
			return CallEndpoint<List<Agreement>> ("agreements", prepareRequest);
		}

		public List<Payment> Payments (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Payment>> ("payments", prepareRequest);
		}

		public List<Branding> Branding (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Branding>> ("brandings", prepareRequest);
		}

		public List<Activity> Activity (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Activity>> ("activity", prepareRequest);
		}

		public List<AcquirerStatus> AcquirerOperationalStatus ()
		{
			return CallEndpoint<List<AcquirerStatus>> ("operational-status/acquirers");
		}

		protected RestRequest CreateRequest (string resource)
		{
			var request = new RestRequest (resource);
			request.AddHeader ("Accept-Version", "v10");
			request.AddHeader ("accept", "application/json, text/plain, */*");
			return request;
		}

		protected void AddPagingParameters (PageParameters? pageParameters, RestRequest request)
		{
			if (pageParameters == null)
				return;
			request.AddParameter ("page", pageParameters.Value.Page);
			request.AddParameter ("page_size", pageParameters.Value.PageSize);
		}

		protected void AddSortingParameters (SortingParameters? sortingParameters, RestRequest request)
		{
			if (sortingParameters == null)
				return;

			if (sortingParameters.Value.SortBy == String.Empty)
				throw new ArgumentException ("sort_by cannot be empty");

			request.AddParameter ("sort_by", sortingParameters.Value.SortBy);
			request.AddParameter ("sort_dir", sortingParameters.Value.SortDirection.GetName ());
		}

		protected T CallEndpoint<T> (string endpointName, Action<RestRequest> prepareRequest = null) where T: new()
		{
			var request = CreateRequest (endpointName);
			if (prepareRequest != null)
				prepareRequest (request);

			var response = Client.Execute<T> (request);
			VerifyResponse (response);
			return response.Data;	
		}

		protected List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode> () {
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.Accepted
		};

		protected void VerifyResponse<T> (IRestResponse<T> response)
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
