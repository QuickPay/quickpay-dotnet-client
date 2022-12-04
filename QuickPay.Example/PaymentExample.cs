using Quickpay.Models.Payments;
using Quickpay.RequestParams;
using Quickpay.Services;
using QuickPay.RequestParams;
using System;
using System.Text.Json;
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

        // All the requests we need to create, authorize and capture a payment is inclosed in the PaymentService
        private static readonly PaymentsService paymentService = new PaymentsService("<<API_KEY>>");

        private static async Task createPayment()
        {            
            // First we must create a payment and for this we need a CreatePaymentRequestParams object
            var createPaymentParams = new CreatePaymentRequestParams("DKK", createRandomOrderId());
            createPaymentParams.text_on_statement = "QuickPay .NET Example";

            var basketItemJeans = new Basket();
            basketItemJeans.qty = 1;
            basketItemJeans.item_name = "Jeans";
            basketItemJeans.item_price = 100;
            basketItemJeans.vat_rate = 0.25;
            basketItemJeans.item_no = "123";

            var basketItemShirt = new Basket();
            basketItemShirt.qty = 1;
            basketItemShirt.item_name = "Shirt";
            basketItemShirt.item_price = 300;
            basketItemShirt.vat_rate = 0.25;
            basketItemShirt.item_no = "321";

            createPaymentParams.basket = new Basket[] { basketItemJeans, basketItemShirt };

            var payment = await paymentService.CreatePayment(createPaymentParams);
            Console.WriteLine("Payment after creation: " + JsonSerializer.Serialize(payment));


            // Second we must create a payment link for the payment. This payment link can be opened in a browser to show the payment window from QuickPay.
            var createPaymentLinkParams = new CreatePaymentLinkRequestParams((int)((basketItemJeans.qty * basketItemJeans.item_price + basketItemShirt.qty * basketItemShirt.item_price) * 100));
            createPaymentLinkParams.payment_methods = "creditcard";
            createPaymentLinkParams.auto_capture = true; // This will automatically capture the payment right after it has been authorized.

            var paymentLink = await paymentService.CreateOrUpdatePaymentLink(payment.id, createPaymentLinkParams);

            Console.WriteLine("Payment link: " + paymentLink.url);
        }

        private static async Task capturePayment(int paymentId)
        {
            // If auto_capture was false we need to capture the payment manually.
            var capturePaymentParams = new CapturePaymentRequestParams(400);
            var payment = await paymentService.CapturePayment(paymentId, capturePaymentParams);

            Console.WriteLine("Payment after capture: " + JsonSerializer.Serialize(payment));
        }


        private static string createRandomOrderId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        }
    }
}
