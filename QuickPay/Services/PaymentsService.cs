using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using QuickPay.RequestParams;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quickpay.Services
{
    public class PaymentsService : QuickPayRestClient
    {
        public PaymentsService(string username, string password) : base(username, password) { }
        public PaymentsService(string apikey) : base(apikey) { }


		public Task<List<Payment>> GetAllPayments(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters(pageParameters, request);
				AddSortingParameters(sortingParameters, request);
			};

			return CallEndpointAsync<List<Payment>>("payments", prepareRequest);
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

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int id, CreatePaymentLinkRequestParams requestParams)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Put;
                request.AddJsonBody(requestParams);
            };

            return CallEndpointAsync<PaymentLinkUrl>(("payments/" + id + "/link"), prepareRequest);
        }

        public Task DeletePaymentLink(int id)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Delete;
            };

            return CallEndpointAsync<Object>(("payments/" + id + "/link"), prepareRequest);
        }

        public Task<Payment> GetPayment(int id, int? operations_size = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {};
            var url = "payments/" + id;
            if (operations_size != null)
            {
                url += "?operations_size=" + operations_size;
            }

            return CallEndpointAsync<Payment>(url, prepareRequest);
		}

        public Task<Payment> UpdatePayment(int id, UpdatePaymentRequestParams requestParams)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Patch;
                request.AddJsonBody(requestParams);
            };

            return CallEndpointAsync<Payment>("payments/" + id, prepareRequest);
        }

        public Task<Payment> CapturePayment(int id, CapturePaymentRequestParams requestParams, string? callbackUrl = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Post;
                request.AddJsonBody(requestParams);
				if(callbackUrl != null)
                {
					request.AddHeader("QuickPay-Callback-Url", callbackUrl);
                }
            };

            return CallEndpointAsync<Payment>("payments/"+id+"/capture", prepareRequest);
        }

        public Task<Payment> RefundPayment(int id, RefundPaymentRequestParams requestParams, string? callbackUrl = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Post;
                request.AddJsonBody(requestParams);
                if (callbackUrl != null)
                {
                    request.AddHeader("QuickPay-Callback-Url", callbackUrl);
                }
            };

            return CallEndpointAsync<Payment>("payments/" + id + "/refund", prepareRequest);
        }

        public Task<Payment> CancelPayment(int id, string? callbackUrl = null, bool? synchronized = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Post;
                if (callbackUrl != null)
                {
                    request.AddHeader("QuickPay-Callback-Url", callbackUrl);
                }
            };

            var url = "payments/" + id + "/cancel";
            if (synchronized != null)
            {
                url += "?synchronized=" + synchronized;
            }

            return CallEndpointAsync<Payment>(url, prepareRequest);
        }
    }
}
