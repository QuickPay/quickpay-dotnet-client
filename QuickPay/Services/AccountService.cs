using Quickpay.Models.Account;

namespace Quickpay.Services
{
    public class AccountService : QuickPayRestClient
    {
		public AccountService(string username, string password) : base(username, password)
		{
		}

		public AccountService(string apikey) : base(apikey)
		{
		}


		public Merchant GetMerchantAccount()
		{
			return CallEndpoint<Merchant>("account");
		}

		public PrivateKey GetPrivateKeyOfMerchant()
        {
			return CallEndpoint<PrivateKey>("account/private-key");
        }

		public Zero4PlatformSettings Get04PlatformSettings()
		{
			return CallEndpoint<Zero4PlatformSettings>("account/04-platform");
		}
	}
}
