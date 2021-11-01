using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using Quickpay.Services;
using System;

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
        public void CreatePayment()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = createRandomOrderId();

            var reqParans = new CreatePaymentRequestParams("DKK", randomOrderId);

            var result = service.CreatePayment(reqParans);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreatePaymentLink()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = createRandomOrderId();

            var createPaymentReqParans = new CreatePaymentRequestParams("DKK", randomOrderId);
            Payment payment = service.CreatePayment(createPaymentReqParans);

            var createPaymentLinkReqParans = new CreatePaymentLinkRequestParams(payment.id, 1000);
            var result = service.CreateOrUpdatePaymentLink(createPaymentLinkReqParans);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeletePaymentLink()
        {
            var service = new PaymentsService(QpConfig.ApiKey);
            string randomOrderId = createRandomOrderId();

            var createPaymentReqParans = new CreatePaymentRequestParams("DKK", randomOrderId);
            Payment payment = service.CreatePayment(createPaymentReqParans);

            var createPaymentLinkReqParans = new CreatePaymentLinkRequestParams(payment.id, 1000);
            var paymentUrl = service.CreateOrUpdatePaymentLink(createPaymentLinkReqParans);

            service.DeletePaymentLink(payment.id);
        }


        private string createRandomOrderId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        }
    }
}