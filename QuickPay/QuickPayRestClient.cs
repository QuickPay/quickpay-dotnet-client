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

        public QuickPayRestClient(string username, string password)
        {
            Client = new RestClient(BASE_URL)
                {
                    Authenticator = new HttpBasicAuthenticator(username, password),
					UserAgent = "QuickPay .Net client"
                };
        }

        public QuickPayRestClient(string apikey)
        {
			Client = new RestClient(BASE_URL)
			{
				Authenticator = new HttpBasicAuthenticator(string.Empty, apikey)
			};
        }

        private RestRequest CreateRequest(string resource)
        {
            var request = new RestRequest(resource);
            request.AddHeader("Accept-Version", "v10");
            return request;
        }

        public PingResponse Ping()
        {
            var request = CreateRequest("ping");

            var response = Client.Execute<PingResponse> (request);
            VerifyResponse (response);
            return response.Data;
        }

        public async Task<List<AclResource>> AclResourcesAsync(AccountType accountType = AccountType.Any, int page = 1,
                                                               int pageSize = 20)
        {
            var request = CreateRequest("acl-resources");
            request.AddParameter ("page", page);
            request.AddParameter ("page_size", pageSize);
            if(accountType != AccountType.Any)
                request.AddParameter ("account_type", accountType.GetName());
            request.Method = Method.GET;

            var tcs = new TaskCompletionSource<List<AclResource>>();
            Client.ExecuteAsync<List<AclResource>>(request, response => {
                    VerifyResponse (response);
                    tcs.SetResult(response.Data);
                });

            return tcs.Task.Result;
        }

        public List<Payment> Payments()
        {
			var request = CreateRequest("payments");

			var response = Client.Execute<List<Payment>>(request);

            VerifyResponse (response);
			return response.Data;
        }


		public List<Activity> Activity()
		{
			var request = CreateRequest("activity");

			var response = Client.Execute<List<Activity>>(request);

			VerifyResponse (response);
			return response.Data;
		}

		public List<AcquirerStatus> AcquirerOperationalStatus()
		{
			var request = CreateRequest("operational-status/acquirers");

			var response = Client.Execute<List<AcquirerStatus>>(request);

			VerifyResponse (response);
			return response.Data;
		}

        private List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode>(){
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.Accepted
        };

        private void VerifyResponse<T>(IRestResponse<T> response)
        {
			if (response.StatusCode == HttpStatusCode.NotFound) {
				throw new Exception ("Endpoint not found, please note this could mean you are not authorized to access this endpoint");
			}
            if (!OkStatusCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusDescription);
            }
        }
    }
}
