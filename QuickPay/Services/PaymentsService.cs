namespace Quickpay.Services
{
    public class PaymentsService : QuickPayRestClient
    {
		public PaymentsService(string username, string password) : base(username, password)
		{
		}

		public PaymentsService(string apikey) : base(apikey)
		{
		}


		public List<Payment> Payments(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};
			return CallEndpoint<List<Payment>>("payments", prepareRequest);
		}
	}
}
