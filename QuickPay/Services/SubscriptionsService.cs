using System;
using Quickpay;
using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using QuickPay.RequestParams;
using RestSharp;

namespace QuickPay.Services
{
    public class SubscriptionsService : QuickPayRestClient
    {
        public SubscriptionsService(string username, string password) : base(username, password) { }
        public SubscriptionsService(string apikey) : base(apikey) { }


        public Task<Subscription> CreateSubscription(CreateSubscriptionRequestParams requestParams)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Post;
                request.AddJsonBody(requestParams);
            };

            return CallEndpointAsync<Subscription>("subscriptions", prepareRequest);
        }

        public Task<PaymentLinkUrl> CreateOrUpdatePaymentLink(int id, CreatePaymentLinkSubscriptionRequestParams requestParams)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Put;
                request.AddJsonBody(requestParams);
            };

            return CallEndpointAsync<PaymentLinkUrl>("subscriptions/" + id + "/link", prepareRequest);
        }

        public Task DeletePaymentLink(int id)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Delete;
            };

            return CallEndpointAsync<Object>(("subscriptions/" + id + "/link"), prepareRequest);
        }

        public Task<Subscription> GetSubscription(int id, int? operations_size = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) => {};
            var url = "subscriptions/" + id;
            if(operations_size != null)
            {
                url += "?operations_size=" + operations_size;
            }

            return CallEndpointAsync<Subscription>(url, prepareRequest);
        }

        public Task<List<Subscription>> GetAllSubscriptions(PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) => {
                AddPagingParameters(pageParameters, request);
                AddSortingParameters(sortingParameters, request);
            };

            return CallEndpointAsync<List<Subscription>>("subscriptions", prepareRequest);
        }

        public Task<Subscription> UpdateSubscription(int id, UpdateSubscriptionRequestParams requestParams)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Patch;
                request.AddJsonBody(requestParams);
            };

            return CallEndpointAsync<Subscription>("subscriptions/" + id, prepareRequest);
        }

        public Task<Subscription> CancelSubscription(int id, string? callbackUrl = null, bool? synchronized = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) =>
            {
                request.Method = Method.Post;
                if (callbackUrl != null)
                {
                    request.AddHeader("QuickPay-Callback-Url", callbackUrl);
                }
            };

            var url = "subscriptions/" + id + "/cancel";
            if(synchronized != null)
            {
                url += "?synchronized=" + synchronized;
            }

            return CallEndpointAsync<Subscription>(url, prepareRequest);
        }

        public Task<Subscription> CreateSubscriptionRecurringPayment(int id, CreateSubscriptionRecurringPaymentRequestParams requestParams, string? callbackUrl = null)
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

            return CallEndpointAsync<Subscription>("subscriptions/" + id + "/recurring", prepareRequest);
        }

        public Task<List<Payment>> GetAllSubscriptionPayments(int id, PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
        {
            Action<RestRequest> prepareRequest = (RestRequest request) => {
                AddPagingParameters(pageParameters, request);
                AddSortingParameters(sortingParameters, request);
            };

            return CallEndpointAsync<List<Payment>>("subscriptions/" + id + "/payments", prepareRequest);
        }
    }
}