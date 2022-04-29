using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using Quickpay.Services;
using System;
using System.Threading.Tasks;

namespace QuickPay.Example
{
    /**
     * QuickPay .NET SDK Example project
     * The purpose of this example project is to show how you can create a payment and genereate a payment link for authorization.
     */
    internal class PaymentExample
    {
        static void Main(string[] args)
        {
            createPayment().GetAwaiter().GetResult();
        }

        private static async Task createPayment()
        {
            // All the requests we need to create a payment is inclosed in the PaymentService
            var paymentService = new PaymentsService("<<API_KEY>>");


            // First we must create a payment and for this we need a CreatePaymentRequestParams object
            var createPaymentParams = new CreatePaymentRequestParams("DKK", createRandomOrderId());

            var basketItemJeans = new Basket();
            basketItemJeans.qty = 2;
            basketItemJeans.item_name = "Jeans";
            basketItemJeans.item_price = 499;
            basketItemJeans.vat_rate = 0.25;
            basketItemJeans.item_no = "123";

            var basketItemShirt = new Basket();
            basketItemShirt.qty = 3;
            basketItemShirt.item_name = "Shirt";
            basketItemShirt.item_price = 250;
            basketItemShirt.vat_rate = 0.25;
            basketItemShirt.item_no = "321";

            createPaymentParams.basket = new Basket[] { basketItemJeans, basketItemShirt };

            var payment = await paymentService.CreatePayment(createPaymentParams);


            // Second we must create a payment link for the payment. This payment link can be opened in a browser to show the payment window from QuickPay.
            var createPaymentLinkParams = new CreatePaymentLinkRequestParams(payment.id, (int)((basketItemJeans.qty * basketItemJeans.item_price + basketItemShirt.qty * basketItemShirt.item_price) * 100));

            var paymentLink = await paymentService.CreateOrUpdatePaymentLink(createPaymentLinkParams);

            Console.WriteLine("Payment URL: " + paymentLink.url);
        }


        private static string createRandomOrderId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        }
    }
}
