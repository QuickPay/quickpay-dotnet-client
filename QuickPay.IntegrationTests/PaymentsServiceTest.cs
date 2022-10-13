using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void GetPayments()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            var result = service.GetAllPayments();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSpecificPayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            var result = service.GetPayment(270677180);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateBasicPayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreatePaymentRequestParams("DKK", randomOrderId);

            var result = service.CreatePayment(reqParams);
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

            var result = service.CreatePayment(reqParams);
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

            var result = service.CreatePayment(reqParams);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePaymentLink()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var createPaymentReqParams= new CreatePaymentRequestParams("DKK", randomOrderId);
            Payment payment = service.CreatePayment(createPaymentReqParams).GetAwaiter().GetResult();

            var createPaymentLinkReqParams = new CreatePaymentLinkRequestParams(1000);
            var result = service.CreateOrUpdatePaymentLink(payment.id, createPaymentLinkReqParams);

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

            service.DeletePaymentLink(payment.id);
        }
    }
}