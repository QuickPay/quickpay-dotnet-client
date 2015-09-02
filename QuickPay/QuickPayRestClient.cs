using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using RestSharp;

namespace Quickpay
{
    public class QuickPayRestClient
    {
        public RestClient Client { get; set; }
		public string BaseUrl { get; set; }
		public const string BASE_URL = "https://api.quickpay.net/";

        public QuickPayRestClient(string username, string password)
        {
			if (BaseUrl == string.Empty) {
				BaseUrl = BASE_URL;
			}

            Client = new RestClient("https://api.quickpay.net/")
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public QuickPayRestClient(string apikey)
        {
			Client = new RestClient ("https://api.quickpay.net/");
			Client.AddDefaultParameter("Authorization", string.Format("Basic {0}", ToSecret(":" + apikey)));
        }

        private RestRequest CreateRequest(string resource)
        {
            var request = new RestRequest(resource, Method.POST);
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

		public List<AclResource> AclResources(AccountType accountType = AccountType.Any, int page = 1,
			int pageSize = 20)
        {
            var request = CreateRequest("acl-resources");
			request.AddParameter ("page", page);
			request.AddParameter ("page_size", pageSize);
			if(accountType != AccountType.Any)
			  request.AddParameter ("account_type", GetName(accountType));
            request.Method = Method.GET;

            var response =  Client.Execute<List<AclResource>>(request);
			VerifyResponse (response);
          
            return response.Data;
        }

		private string GetName(AccountType value)
		{
			return Enum.GetName (typeof(AccountType), value);
		}

					private static string ToSecret(string apikey)
					{
						return Convert.ToBase64String(Encoding.UTF8.GetBytes(apikey));
					}

		private List<HttpStatusCode> OkStatusCodes = new List<HttpStatusCode>(){
			HttpStatusCode.OK,
			HttpStatusCode.Created,
			HttpStatusCode.Accepted
		};

        private void VerifyResponse<T>(IRestResponse<T> response)
        {
			if (!OkStatusCodes.Contains(response.StatusCode))
            {
                throw new Exception(response.StatusDescription);
            }
        }

    }
}
