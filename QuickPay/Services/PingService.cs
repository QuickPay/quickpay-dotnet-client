using Quickpay.Models.Ping;

namespace Quickpay.Services
{
    public class PingService : QuickPayRestClient
    {
		public PingService(string username, string password) : base(username, password)
		{
		}

		public PingService(string apikey) : base(apikey)
		{
		}

		public Pong ping()
        {
			return CallEndpoint<Pong>("ping");
		}
    }
}
