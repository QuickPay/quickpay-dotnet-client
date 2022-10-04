using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		public Task<List<Payment>> GetAllPayments(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};

			return CallEndpointAsync<List<Payment>>("payments", prepareRequest);
		}

		public Task<Payment> GetPayment(int id, PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};

			return CallEndpointAsync<Payment>("payments/" + id, prepareRequest);
		}

		public Task<Payment> CreatePayment(CreatePaymentRequestParams requestParams)
        {
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Post;
				request.AddJsonBody(requestParams);
			};

			return CallEndpointAsync<Payment>("payments", prepareRequest);
        }

		public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(CreatePaymentLinkRequestParams requestParams)
        {
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Put;
				request.AddJsonBody(requestParams);
			};

			return CallEndpointAsync<PaymentLinkUrl>(("payments/"+requestParams.id+"/link"), prepareRequest);
		}

		public Task DeletePaymentLink(int id)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Delete;
			};

			return CallEndpointAsync<Object>(("payments/" + id + "/link"), prepareRequest);
		}
	}
}
