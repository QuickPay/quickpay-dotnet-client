using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;
using Quickpay.Util;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp.Serializers.Json;
using QuickPay.Exceptions;

namespace Quickpay
{
	public abstract class QuickPayRestClient
	{
		#pragma warning disable S1075 // URIs should not be hardcoded
        private const string BASE_URL = "https://api.quickpay.net/";
		#pragma warning restore S1075 // URIs should not be hardcoded

        protected RestClient Client { get; set; }

        protected QuickPayRestClient(string apikey) : this(string.Empty, apikey)
		{
		}

        protected QuickPayRestClient (string username, string password)
		{
			if (password == string.Empty) {
				throw new ArgumentException ("You need to provide either a username / password or an apikey");
			}

			var restClientOptions = new RestClientOptions(BASE_URL)
			{
				UserAgent = "QuickPay .Net Standard 2.0 SDK",
				FollowRedirects = true
			};

			Client = new RestClient(options: restClientOptions) {
				Authenticator = new HttpBasicAuthenticator(username, password)
			};

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            Client.UseSerializer(() =>
            {
                return new SystemTextJsonSerializer(options);
            });
        }


        protected RestRequest CreateRequest (string resource)
		{
			var request = new RestRequest (resource);
			request.AddHeader("Accept-Version", "v10");
			request.AddHeader("Accept", "application/json");
			return request;
		}

		protected void AddPagingParameters(Nullable<PageParameters> pageParameters, RestRequest request)
		{
			if (pageParameters == null)
				return;
			request.AddParameter ("page", pageParameters.Value.Page);
			request.AddParameter ("page_size", pageParameters.Value.PageSize);
		}

		protected void AddSortingParameters(Nullable<SortingParameters> sortingParameters, RestRequest request)
		{
			if (sortingParameters == null)
				return;

			if (sortingParameters.Value.SortBy == String.Empty)
				throw new ArgumentException ("sort_by cannot be empty");

			request.AddParameter ("sort_by", sortingParameters.Value.SortBy);
			request.AddParameter ("sort_dir", sortingParameters.Value.SortDirection.GetName ());
		}

        protected async Task<T> CallEndpointAsync<T> (string endpointName, Action<RestRequest> prepareRequest = null) where T: new()
		{
			var request = CreateRequest (endpointName);
			if (prepareRequest != null)
				prepareRequest (request);

			var cancellationTokenSource = new CancellationTokenSource();
		
			var response = await Client.ExecuteAsync<T>(request, cancellationTokenSource.Token);

			VerifyResponse(response);
			return response.Data;	
		}

		protected List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode> () {
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.Accepted,
			HttpStatusCode.NoContent
		};

		protected void VerifyResponse<T> (RestResponse<T> response)
		{
			if (response.StatusCode == HttpStatusCode.NotFound) {
				throw new NotFoundException ("Endpoint not found, please note this could mean you are not authorized to access this endpoint");
			}

			if (!OkStatusCodes.Contains (response.StatusCode)) {
				throw new HttpFailureStatusCodeException((int)response.StatusCode, response.Content);
			}

			if(response.ErrorException != null) {
				throw response.ErrorException;
            }
		}
	}
}
