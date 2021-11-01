using Quickpay.Models.Account.Settings;

namespace Quickpay.Services
{
    public class AcquirersService : QuickPayRestClient
    {
		public AcquirersService(string username, string password) : base(username, password)
		{
		}

		public AcquirersService(string apikey) : base(apikey)
		{
		}

		public AcquirerSettings FetchAcquirers()
        {
			return CallEndpoint<AcquirerSettings>("acquirers");
		}


	}
}
