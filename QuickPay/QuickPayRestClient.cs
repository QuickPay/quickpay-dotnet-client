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


        public async Task<PingResponse> PingAsync()
        {
            var request = CreateRequest("ping");

            return Client.Execute<PingResponse>(request).Data;
        }

        public List<AclResource> AclResources()
        {
            var request = CreateRequest("acl-resources");
            request.Method = Method.GET;

            var response =  Client.Execute<List<AclResource>>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }
            return response.Data;
        }

    }
}
