using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using RestSharp;
using System;
using System.Collections.Generic;

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


		public List<Payment> GetAllPayments(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};

			return CallEndpoint<List<Payment>>("payments", prepareRequest);
		}

		public Payment GetPayment(int id, PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};

			return CallEndpoint<Payment>("payments/" + id, prepareRequest);
		}

		public Payment CreatePayment(CreatePaymentRequestParams requestParams)
        {
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Post;
				request.AddJsonBody(requestParams);
			};

			return CallEndpoint<Payment>("payments", prepareRequest);
        }

		public PaymentLinkUrl CreateOrUpdatePaymentLink(CreatePaymentLinkRequestParams requestParams)
        {
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Put;
				request.AddJsonBody(requestParams);
			};

			return CallEndpoint<PaymentLinkUrl>(("payments/"+requestParams.id+"/link"), prepareRequest);
		}

		public void DeletePaymentLink(int id)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) =>
			{
				request.Method = Method.Delete;
			};

			CallEndpoint<Object>(("payments/" + id + "/link"), prepareRequest);
		}
	}
}
