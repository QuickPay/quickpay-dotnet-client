using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace Quickpay
{
    public class QuickPayRestClient
    {
        public RestClient Client { get; set; }

        public QuickPayRestClient(string username, string password)
        {
            Client = new RestClient("https://api.quickpay.net/")
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public QuickPayRestClient(string apikey)
        {
            Client = new RestClient("https://api.quickpay.net/")
            {
                Authenticator = new HttpBasicAuthenticator(string.Empty, apikey)
            };
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
