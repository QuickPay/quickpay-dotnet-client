using System;
using QuickPay.IntegrationTests.Util;
using Quickpay.RequestParams;
using Quickpay.Services;
using QuickPay.Services;

namespace QuickPay.IntegrationTests
{
    [TestClass]
    public class SubscriptionsServiceTest
    {
        [TestMethod]
        public void CreateBasicSubscription()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreateSubscriptionRequestParams("DKK", randomOrderId, "Abonnement");

            var task = service.CreateSubscription(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSubscription()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreateSubscriptionRequestParams("DKK", randomOrderId, "Abonnement");

            var task = service.CreateSubscription(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);

            task = service.GetSubscription(result.id);
            var result2 = task.Result;
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public void GetAllSubscription()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);

            var task = service.GetAllSubscriptions();
            var result = task.Result;

            Assert.IsNotNull(result);   
        }

        [TestMethod]
        public void CreatePaymentLink()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreateSubscriptionRequestParams("DKK", randomOrderId, "Abonnement");

            var task = service.CreateSubscription(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);

            var paymentLinkReqParams = new CreatePaymentLinkSubscriptionRequestParams(1000);
            var task2 = service.CreateOrUpdatePaymentLink(result.id, paymentLinkReqParams);
            var result2 = task2.Result;
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public void DeletePaymentLink()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreateSubscriptionRequestParams("DKK", randomOrderId, "Abonnement");

            var task = service.CreateSubscription(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);

            var paymentLinkReqParams = new CreatePaymentLinkSubscriptionRequestParams(1000);
            var task2 = service.CreateOrUpdatePaymentLink(result.id, paymentLinkReqParams);
            var result2 = task2.Result;
            Assert.IsNotNull(result2);

            service.DeletePaymentLink(result.id);
        }

        [TestMethod]
        public void UpdateSubscription()
        {
            var service = new SubscriptionsService(QpConfig.ApiKey);
            string randomOrderId = OrderIdGenerator.createRandomOrderId();

            var reqParams = new CreateSubscriptionRequestParams("DKK", randomOrderId, "Abonnement");

            var task = service.CreateSubscription(reqParams);
            var result = task.Result;
            Assert.IsNotNull(result);



            var updateSubscriptionReqParam = new UpdateSubscriptionRequestParams()
            {
                description = "New description"
            };
            var task2 = service.UpdateSubscription(result.id, updateSubscriptionReqParam);
            var result2 = task2.Result;
            Assert.IsNotNull(result2);
            Assert.AreEqual(result2.description, updateSubscriptionReqParam.description);
        }
    }
}
