using Quickpay.Models.Account.Settings;
using System.Threading.Tasks;

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

		public Task<AcquirerSettings> FetchAcquirers()
        {
			return CallEndpointAsync<AcquirerSettings>("acquirers");
		}


	}
}
