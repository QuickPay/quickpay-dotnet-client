using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Quickpay
{
    public class QuickPayRestClient
    {
        public RestClient Client { get; set; }
        public QuickPayRestClient(string username, string password, string apikey="")
        {
            Client = new RestClient("https://api.quickpay.net/")
            {
                Authenticator =
                    apikey !=string.Empty
                        ? new HttpBasicAuthenticator(string.Empty, apikey)
                        : new HttpBasicAuthenticator(username, password)
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

            return Client.Execute<PingResponse>(request).Data;
        }
    }
}
