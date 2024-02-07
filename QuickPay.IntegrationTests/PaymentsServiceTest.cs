using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quickpay;
using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using Quickpay.Services;
using QuickPay.IntegrationTests.Util;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class PaymentsServiceTest
    {
        [TestMethod]
        public void GetAllPayments()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            var task = service.GetAllPayments();
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllPaymentsWithPaging()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            var pageParams = new PageParameters()
            {
                Page = 1,
                PageSize = 10
            };
            var task = service.GetAllPayments(pageParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSpecificPayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            var task = service.GetPayment(270677180);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateBasicPayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreatePaymentRequestParams("DKK", randomOrderId);

            var task = service.CreatePayment(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePaymentWithBasket()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreatePaymentRequestParams("DKK", randomOrderId);

            var basket = new Basket();
            basket.qty = 2;
            basket.item_name = "Shirt";
            basket.vat_rate = 0.25;
            basket.item_price = 100;
            basket.item_no = "1234";

            reqParams.basket = new Basket[] { basket };

            var task = service.CreatePayment(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePaymentWithAddress()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreatePaymentRequestParams("DKK", randomOrderId);
            reqParams.invoice_address = new OptionalAddress();
            reqParams.invoice_address.country_code = "DNK";
            reqParams.invoice_address.phone_number = "12345678";
            reqParams.invoice_address.name = "John Doe";
            reqParams.invoice_address.house_number = "42";

            var task = service.CreatePayment(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePaymentLink()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var createPaymentReqParams = new CreatePaymentRequestParams("DKK", randomOrderId);
            Payment payment = service.CreatePayment(createPaymentReqParams).GetAwaiter().GetResult();

            var createPaymentLinkReqParams = new CreatePaymentLinkRequestParams(1000);
            var task = service.CreateOrUpdatePaymentLink(payment.id, createPaymentLinkReqParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeletePaymentLink()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var createPaymentReqParams = new CreatePaymentRequestParams("DKK", randomOrderId);
            Payment payment = service.CreatePayment(createPaymentReqParams).GetAwaiter().GetResult();

            var createPaymentLinkReqParams = new CreatePaymentLinkRequestParams(1000);
            var paymentUrl = service.CreateOrUpdatePaymentLink(payment.id, createPaymentLinkReqParams);

            var task = service.DeletePaymentLink(payment.id);

            try
            {
                task.GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                Assert.Fail("No exception should be thrown");
            }
        }

        [TestMethod]
        public void UpdatePayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreatePaymentRequestParams("DKK", randomOrderId);

            var task = service.CreatePayment(reqParams);
            var payment = task.Result;

            Assert.IsNotNull(payment);

            var updateReqParams = new UpdatePaymentRequestParams();
            var basket = new Basket();
            basket.qty = 2;
            basket.item_name = "Shirt";
            basket.vat_rate = 0.25;
            basket.item_price = 100;
            basket.item_no = "1234";

            updateReqParams.basket = new Basket[] { basket };

            task = service.UpdatePayment(payment.id, updateReqParams);
            var result = task.Result;

            Assert.IsNotNull(result);
        }

        private readonly string _jsonPaymentResponseTest = @"{
		""id"": 2,
		""type"": ""authorize"",
		""amount"": 148,
		""pending"": false,
		""qp_status_code"": ""20000"",
		""qp_status_msg"": ""Approved"",
		""aq_status_code"": ""0"",
		""aq_status_msg"": ""Accepted, The agreement has been accepted."",
		""data"": { },
		""callback_url"": ""https://13f5-147-78-28-145.ngrok-free.app/subscriptions/callback"",
		""callback_success"": false,
		""callback_response_code"": null,
		""callback_duration"": 30002,
		""acquirer"": ""mobilepaysubscriptions"",
		""3d_secure_status"": null,
		""callback_at"": ""2023-09-06T11:54:00Z"",
		""created_at"": ""2023-09-06T11:50:58Z""
	}";

        [TestMethod]
        public void ParseJsonResponse()
        {
            try
            {
                var operation = System.Text.Json.JsonSerializer.Deserialize<Quickpay.Models.Payments.Operation>(_jsonPaymentResponseTest);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}