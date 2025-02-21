using System.Threading.Tasks;
using QuickPay.Models.Account;

namespace QuickPay.Services
{
    public class AccountService : QuickPayRestClient
    {
		public AccountService(string username, string password) : base(username, password)
		{
		}

		public AccountService(string apikey) : base(apikey)
		{
		}


		public Task<Merchant> GetMerchantAccount()
		{
			return CallEndpointAsync<Merchant>("account");
		}

		public Task<PrivateKey> GetPrivateKeyOfMerchant()
        {
			return CallEndpointAsync<PrivateKey>("account/private-key");
        }

	}
}
