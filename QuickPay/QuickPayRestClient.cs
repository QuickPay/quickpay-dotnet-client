using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;
using Quickpay.Util;


namespace Quickpay
{
	public abstract class QuickPayRestClient
	{
        private const string BASE_URL = "https://api.quickpay.net/";

		protected RestClient Client { get; set; }


		#region Constructors
		public QuickPayRestClient(string apikey) : this(string.Empty, apikey)
		{
			// NOP
		}

		public QuickPayRestClient (string username, string password)
		{
			if (password == string.Empty) {
				throw new ArgumentException ("You need to provide either a username / password or an apikey");
			}
			Client = new RestClient (BASE_URL) {
				Authenticator = new HttpBasicAuthenticator (username, password),
				UserAgent = "QuickPay .Net Framework 4.8 Client"
			};
		}
        #endregion


        protected RestRequest CreateRequest (string resource)
		{
			var request = new RestRequest (resource);
			request.AddHeader("Accept-Version", "v10");
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json");
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
			Console.WriteLine(response.Content); // TODO: Delete in production
			VerifyResponse (response);
			return response.Data;	
		}


		protected async Task<T> CallEndpointAsync<T> (string endpointName, Action<RestRequest> prepareRequest = null) where T: new()
		{
			var request = CreateRequest (endpointName);
			if (prepareRequest != null)
				prepareRequest (request);

			var cancellationTokenSource = new CancellationTokenSource();
		
			var response = await Client.ExecuteAsync<T>(request, cancellationTokenSource.Token);

			VerifyResponse (response);
			return response.Data;	
		}

		protected List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode> () {
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.Accepted,
			HttpStatusCode.NoContent
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
