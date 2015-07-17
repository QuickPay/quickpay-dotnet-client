using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Quickpay
{
    /// <summary>
    ///     Client to connect to the quickpay API
    /// </summary>
    public class QuickpayClient
    {
        private const string UserAgent = "Csharp Quickpay API lib";
        private const string ApiVersion = "v10";
        public const string BASE_URL = "https://api.quickpay.net/";
        private readonly string _apikey;

        /// <summary>
        ///     Create new client using specific key
        /// </summary>
        public QuickpayClient(string apikey)
        {
            _apikey = apikey;
            BaseUrl = BASE_URL;
        }

        /// <summary>
        ///     Set specific timeout, or leave at 0 or negative to use default.
        /// </summary>
        public int TimeoutInMilliseconds { get; set; }

        /// <summary>
        ///     URL used to connect to server.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        ///     If using proxy, set to specific proxy or WebRequest.GetSystemWebProxy or WebRequest.DefaultWebProxy
        /// </summary>
        public WebProxy Proxy { get; set; }

        private static string ToSecret(string apikey)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(apikey));
        }

        internal static object MakeEntry(object o, object def)
        {
            if (ReferenceEquals(o, null) || o.Equals(def))
                return null;
            return o;
        }

        internal static object MakeEntry2(object o, object def)
        {
            if (ReferenceEquals(o, null) || o.Equals(def))
                return null;
            if (o is bool)
                return (bool) o ? 1 : 0;
            var list = o as List<string>;
            if (list != null)
                return string.Join(",", list);
            return o;
        }

        private static string DictionaryToQueryString(Dictionary<string, object> param)
        {
            if (param == null)
                return null;
            var all = param.Where(x => x.Value != null).ToList();
            if (all.Count == 0)
                return null;
            var sb = new StringBuilder();
            var first = true;
            foreach (var kvp in all)
            {
                if (!first) sb.Append("&");
                first = false;
                sb.AppendFormat("{0}={1}", 
                    HttpUtility.UrlEncode(kvp.Key), 
                    HttpUtility.UrlEncode(kvp.Value.ToString()));
            }
            return sb.ToString();
        }

        private Dictionary<string, object> CleanParam(Dictionary<string, object> param)
        {
            return param == null ? null : param.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
        }

        private HttpWebRequest CreateClient(string urlfragment, HttpVerb method = HttpVerb.GET,
            Dictionary<string, object> param = null)
        {
            var uri = BaseUrl + urlfragment;
            param = CleanParam(param);
            var hasParam = param != null && param.Count > 0;
            if (method == HttpVerb.GET && hasParam)
                uri += "?" + DictionaryToQueryString(param);
            var client = WebRequest.CreateHttp(uri);
            client.Headers["Accept-Version"] = ApiVersion;
            client.UserAgent = UserAgent;
            client.Headers["Authorization"] = string.Format("Basic {0}", ToSecret(":" + _apikey));
            client.Method = method.ToString();
            client.AutomaticDecompression = DecompressionMethods.GZip;
            client.KeepAlive = false;
            client.Proxy = Proxy;
            if (TimeoutInMilliseconds > 0)
                client.Timeout = TimeoutInMilliseconds;
            if (method == HttpVerb.GET || !hasParam)
                return client;
            client.ContentType = "application/json";
            var jsonParam = JsonConvert.SerializeObject(param);
            var rawbody = Encoding.UTF8.GetBytes(jsonParam);
            client.ContentLength = rawbody.Length;
            var dataStream = client.GetRequestStream();
            dataStream.Write(rawbody, 0, rawbody.Length);
            dataStream.Close();
            return client;
        }

        private static async Task<T> RunRequest<T>(HttpWebRequest client, Func<HttpWebResponse, T> processReturn,
            CancellationToken cancellationToken)
        {
            return await client.GetResponseAsync()
                .ContinueWith(t =>
                {
                    try
                    {
                        var exception = t.Exception;
                        var e = exception == null ? null : exception.GetBaseException() as WebException;
                        if (e != null)
                        {
                            using (var res = e.Response)
                            using (var stream = res.GetResponseStream())
                            using (var readStream = new StreamReader(stream, Encoding.UTF8))
                            {
                                var readToEnd = readStream.ReadToEnd();
                                throw new QuickpayException(readToEnd, e);
                            }
                        }
                    }
                    catch (QuickpayException)
                    {
                        throw;
                    }
                    catch
                    {
                        // ignored
                    }
                    return processReturn((HttpWebResponse) t.Result);
                }, cancellationToken).ConfigureAwait(false);
        }

        private static string ResponseAsString(HttpWebResponse r)
        {
            using (var stream = r.GetResponseStream())
            using (var readStream = new StreamReader(stream, Encoding.UTF8))
                return readStream.ReadToEnd();
        }

        private static T SerializeResponse<T>(HttpWebResponse r)
        {
            var serializer = new JsonSerializer();
            using (var stream = r.GetResponseStream())
            using (var readStream = new StreamReader(stream, Encoding.UTF8))
            {
                var readToEnd = readStream.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(readToEnd);
            }
        }

        /// <summary>
        ///     Ping the server. <see cref="http://tech.quickpay.net/api/services/?scope=anonymous#ping" />
        /// </summary>
        /// <param name="usePost">Use the POST method ping</param>
        /// <param name="useAuthentication">Use authentication</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>
        ///     True if server gave the expected result. In most cases exception if it didnt. Notice the default api user does
        ///     not have rights to PING the server.
        /// </returns>
        public async Task<bool> Ping(bool usePost = false, bool useAuthentication = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = CreateClient("ping", usePost ? HttpVerb.POST : HttpVerb.GET);
            if (!useAuthentication)
                client.Headers.Remove("Authorization");
            var expectedResult = usePost ? HttpStatusCode.Created : HttpStatusCode.OK;
            return await RunRequest(client, r => r.StatusCode == expectedResult, cancellationToken);
        }

        /// <summary>
        ///     Get the ACL resources. <see cref="http://tech.quickpay.net/api/services/?scope=anonymous#acl-resources" />
        /// </summary>
        public async Task<List<AclResource>> GetAclResource(AccountType accountType = AccountType.Any, int page = 1,
            int pageSize = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"account_type", MakeEntry(accountType, AccountType.Any)},
                {"page", MakeEntry(page, 1)},
                {"page_size", MakeEntry(pageSize, 20)}
            };
            return
                await
                    RunRequest(CreateClient("acl-resources", param: param), SerializeResponse<List<AclResource>>,
                        cancellationToken);
        }

        /// <summary>
        ///     Get the changelog. <see cref="http://tech.quickpay.net/api/services/?scope=anonymous#changelog" />
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        public async Task<string> Changelog(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await RunRequest(CreateClient("changelog"), ResponseAsString, cancellationToken);
        }

        /// <summary>
        ///     Create the final dictionary with all keys including checksum. Use in <see cref="CreatePaymentWindowForm" /> to get
        ///     the whole form.
        ///     See http://tech.quickpay.net/payments/hosted/ for individual parameter documentation.
        ///     Note: This function doesn't do any validation, that is handled by the server.
        /// </summary>
        public Dictionary<string, object> CreatePaymentWindowFormVariables(uint merchantId, uint agreementId,
            string orderId, uint amount,
            string currency, string continueurl, string cancelurl,
            string language, string callbackurl = null, bool? autocapture = null,
            bool? autofee = null, bool? subscription = null, string description = null,
            List<string> paymentMethods = null, string acquirer = null,
            int brandingId = 0, int googleAnalyticsTrackingId = 0, int googleAnalyticsClientId = 0,
            Dictionary<string, string> extraParameters = null)
        {
            var param = new Dictionary<string, object>
            {
                {"version", ApiVersion},
                {"merchant_id", merchantId},
                {"order_id", orderId},
                {"amount", amount},
                {"currency", currency},
                {"continueurl", continueurl},
                {"cancelurl", MakeEntry(cancelurl, null)},
                {"language", language},
                {"autocapture", MakeEntry2(autocapture, null)},
                {"autofee", MakeEntry2(autofee, null)},
                {"subscription", MakeEntry2(subscription, null)},
                {"description", MakeEntry(description, null)},
                {"payment_methods", MakeEntry2(paymentMethods, null)},
                {"acquirer", MakeEntry(acquirer, null)},
                {"branding_id", MakeEntry(brandingId, 0)},
                {"google_analytics_tracking_id", MakeEntry(googleAnalyticsTrackingId, 0)},
                {"google_analytics_client_id", MakeEntry(googleAnalyticsClientId, 0)}
            };
            if (extraParameters != null)
                foreach (var kvp in extraParameters)
                    param["variables[" + kvp.Key + "]"] = kvp.Value;

            return CleanAndAddChecksumToPaymentWindowVariables(param);
        }

        /// <summary>
        ///     Remove unused entries and then calculate checksum and return the final dictionary.
        /// </summary>
        public Dictionary<string, object> CleanAndAddChecksumToPaymentWindowVariables(Dictionary<string, object> param)
        {
            var all = param.Where(x => x.Value != null).OrderBy(x => x.Key, StringComparer.InvariantCulture).ToList();
            var checksumBase = string.Join(" ", all.Select(c => c.Value));
            var e = Encoding.UTF8;
            var hmac = new HMACSHA256(e.GetBytes(_apikey));
            var sb = new StringBuilder();
            foreach (var b in hmac.ComputeHash(e.GetBytes(checksumBase)))
                sb.Append(b.ToString("x2"));

            var ret = all.ToDictionary(x => x.Key, x => x.Value);
            ret["checksum"] = sb.ToString();
            return ret;
        }

        /// <summary>
        ///     Create a html form that when submitted will open the payment window
        /// </summary>
        public string CreatePaymentWindowForm(Dictionary<string, string> variables, string submitText, string formId,
            string url = "https://payment.quickpay.net")
        {
            var sb = new StringBuilder();
            sb.Append("<form method=\"POST\" action=\"" + url + "\" id='" + formId + "'>");
            foreach (var kvp in variables)
                sb.Append("<input type=\"hidden\" name=\"" + HttpUtility.HtmlEncode(kvp.Key) + "\" value=\"" +
                          HttpUtility.HtmlEncode(kvp.Value) + "\">");
            sb.Append("<input type=\"submit\" value=\"" + submitText + "\">");
            sb.Append("</form>");
            return sb.ToString();
        }

        /// <summary>
        ///     Create all parameters for payment link.
        ///     <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payment-links" />
        /// </summary>
        public Dictionary<string, object> CreatePaymentLinkDictionary(uint agreementId,
            int transactionId, uint amount,
            string currency, string language, string continueurl = null,
            string cancelurl = null, string callbackurl = null, bool? autocapture = null,
            bool? autofee = null, bool? subscription = null, string description = null,
            List<string> paymentMethods = null, string acquirer = null,
            int brandingId = 0, int googleAnalyticsTrackingId = 0, int googleAnalyticsClientId = 0,
            Dictionary<string, object> extraParameters = null)
        {
            var param = new Dictionary<string, object>
            {
                {"agreement_id", agreementId},
                {"transaction_id", transactionId},
                {"amount", amount},
                {"currency", currency},
                {"continueurl", continueurl},
                {"cancelurl", MakeEntry(cancelurl, null)},
                {"language", language},
                {"autocapture", MakeEntry2(autocapture, null)},
                {"autofee", MakeEntry2(autofee, null)},
                {"subscription", MakeEntry2(subscription, null)},
                {"description", MakeEntry(description, null)},
                {"payment_methods", MakeEntry2(paymentMethods, null)},
                {"acquirer", MakeEntry(acquirer, null)},
                {"branding_id", MakeEntry(brandingId, 0)},
                {"google_analytics_tracking_id", MakeEntry(googleAnalyticsTrackingId, 0)},
                {"google_analytics_client_id", MakeEntry(googleAnalyticsClientId, 0)}
            };
            if (extraParameters != null)
                foreach (var kvp in extraParameters)
                    throw new NotImplementedException("Not currently supported in API");
            //param["variables[" + kvp.Key + "]"] = kvp.Value;
            return param;
        }

        /// <summary>
        ///     Create a new payment link. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payment-links" />
        /// </summary>
        public async Task<PaymentLink> CreatePaymentLink(Dictionary<string, object> param,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                await
                    RunRequest(CreateClient("payment-links", HttpVerb.POST, param), SerializeResponse<PaymentLink>,
                        cancellationToken);
        }

        /// <summary>
        ///     Get info about an existing payment link.
        ///     <see cref="http://tech.quickpay.net/api/services/?scope=anonymous#payment-links" />
        /// </summary>
        public async Task<PaymentLink> GetPaymentLink(string token,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                await
                    RunRequest(CreateClient("payment-links/" + HttpUtility.UrlEncode(token)),
                        SerializeResponse<PaymentLink>, cancellationToken);
        }

        /// <summary>
        ///     Create a new payment. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />
        /// </summary>
        public async Task<Payment> CreatePayment(string currency, string orderId,
            QuickPayPhysicalAddress invoiceAddress = null, QuickPayPhysicalAddress shippingAddress = null,
            AdditionalPaymentData metadata = null, int? brandingId = null,
            Dictionary<string, object> extraParameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"order_id", orderId},
                {"currency", currency}
            };
            if (invoiceAddress != null)
                invoiceAddress.AddToDictionary("invoice_address", param);
            if (shippingAddress != null)
                shippingAddress.AddToDictionary("shipping_address", param);
            if (metadata != null)
                throw new NotImplementedException("Not documented properly how to send this in API");
            if (extraParameters != null)
                foreach (var kvp in extraParameters)
                    throw new NotImplementedException("Not documented properly how to send these in API");
            return
                await
                    RunRequest(CreateClient("payments", HttpVerb.POST, param), SerializeResponse<Payment>,
                        cancellationToken);
        }

        /// <summary>
        ///     Get list of all payments. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />
        /// </summary>
        public async Task<List<Payment>> GetPayments(bool? accepted = null,
            string orderId = null, string sortBy = "id", string sortDir = "asc",
            DateTime? since = null, int page = 1, int pageSize = 20,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"accepted", MakeEntry2(accepted, null)},
                {"order_id", MakeEntry(orderId, null)},
                {"sort_by", MakeEntry(sortBy, "id")},
                {"sort_dir", MakeEntry(sortDir, "asc")},
                {"page", MakeEntry(page, 1)},
                {"page_size", MakeEntry(pageSize, 20)}
            };
            if (since.HasValue)
            {
                var s = since.Value;
                param["date[year]"] = s.Year;
                param["date[month]"] = s.Month;
                param["date[day]"] = s.Day;
                param["date[hour]"] = s.Hour;
                param["date[minute]"] = s.Minute;
            }
            return
                await
                    RunRequest(CreateClient("payments", param: param), SerializeResponse<List<Payment>>,
                        cancellationToken);
        }

        /// <summary>
        ///     Get info about specific payment. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />
        /// </summary>
        public async Task<Payment> GetPayment(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                await
                    RunRequest(CreateClient("payments/" + HttpUtility.UrlEncode(id.ToString())),
                        SerializeResponse<Payment>, cancellationToken);
        }

        /// <summary>
        ///     Capture amount on payment. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />.
        ///     If called with syncronized, extra sanity checks are performed
        /// </summary>
        public async Task<Payment> CapturePayment(int id, uint amount, bool syncronized = false,
            Dictionary<string, object> extraParameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"id", id},
                {"amount", amount}
            };
            if (extraParameters != null)
                foreach (var kvp in extraParameters)
                    throw new NotImplementedException("Not documented properly how to send these in API");
            return
                await
                    RunRequest(
                        CreateClient(
                            "payments/" + HttpUtility.UrlEncode(id.ToString()) + "/capture" +
                            (syncronized ? "?synchronized" : ""), HttpVerb.POST, param),
                        response =>
                        {
                            var payment = SerializeResponse<Payment>(response);
                            if (syncronized)
                            {
                                var lastOperation = payment.Operations.OrderByDescending(x => x.Id).FirstOrDefault();
                                if (lastOperation == null || lastOperation.CreatedAt < DateTime.Now.AddMinutes(-5))
                                    throw new QuickpayException(
                                        "Last payment is unknown or was more than 5 min ago - WARNING! Payment MAY have been done already");
                                if (lastOperation.Amount != amount)
                                    throw new QuickpayException(
                                        "Payment amount is different than what we just paid - WARNING! Payment MAY have been done already");
                                if (lastOperation.Pending)
                                    throw new QuickpayException(
                                        "Syncronious capture should not be pending - WARNING! Payment MAY have been done already");
                                if (lastOperation.QpStatusMsg != "Approved")
                                    throw new QuickpayException("Payment was not approved:" + lastOperation.QpStatusMsg);
                            }
                            return payment;
                        }, cancellationToken);
        }

        /// <summary>
        ///     Capture amount on payment. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />.
        /// </summary>
        public async Task<bool> RefundPayment(int id, uint amount, bool syncronized = false,
            Dictionary<string, object> extraParameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"id", id},
                {"amount", amount}
            };
            if (extraParameters != null)
                foreach (var kvp in extraParameters)
                    throw new NotImplementedException("Not documented properly how to send these in API");
            return
                await
                    RunRequest(
                        CreateClient(
                            "payments/" + HttpUtility.UrlEncode(id.ToString()) + "/refund" +
                            (syncronized ? "?synchronized" : ""), HttpVerb.POST, param),
                        response => response.StatusCode == HttpStatusCode.Accepted, cancellationToken);
        }

        /// <summary>
        ///     Capture amount on payment. <see cref="http://tech.quickpay.net/api/services/?scope=merchant#payments" />.
        /// </summary>
        public async Task<bool> CancelPayment(int id, bool syncronized = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var param = new Dictionary<string, object>
            {
                {"id", id}
            };
            return
                await
                    RunRequest(
                        CreateClient(
                            "payments/" + HttpUtility.UrlEncode(id.ToString()) + "/cancel" +
                            (syncronized ? "?synchronized" : ""), HttpVerb.POST, param),
                        response => response.StatusCode == HttpStatusCode.Accepted, cancellationToken);
        }
    }
}