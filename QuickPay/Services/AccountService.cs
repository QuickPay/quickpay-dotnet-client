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


		public Merchant GET_Account()
		{
			return CallEndpoint<Merchant>("account");
		}

		public PrivateKey GET_PrivateKey()
        {
			return CallEndpoint<PrivateKey>("account/private-key");
        }

		public Zero4PlatformSettings GET_04Platform()
		{
			return CallEndpoint<Zero4PlatformSettings>("account/04-platform");
		}




		/*


		public List<AclResource> AclResources(AccountType accountType = AccountType.Any, PageParameters? pageParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				if (accountType != AccountType.Any)
					request.AddParameter("account_type", accountType.GetName());
			};

			return CallEndpoint<List<AclResource>>("acl-resources", prepareRequest);
		}

		public List<Agreement> Agreements(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};
			return CallEndpoint<List<Agreement>>("agreements", prepareRequest);
		}

		public List<Payment> Payments(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};
			return CallEndpoint<List<Payment>>("payments", prepareRequest);
		}

		public List<Branding> Branding(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};
			return CallEndpoint<List<Branding>>("brandings", prepareRequest);
		}

		public List<Activity> Activity(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};
			return CallEndpoint<List<Activity>>("activity", prepareRequest);
		}

		public List<AcquirerStatus> AcquirerOperationalStatus()
		{
			return CallEndpoint<List<AcquirerStatus>>("operational-status/acquirers");
		}

		*/
	}
}
