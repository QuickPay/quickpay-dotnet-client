using System;
using System.Net;

namespace Quickpay
{
    /// <summary>
    ///     QuickPay Exception
    /// </summary>
    public class QuickpayException : Exception
    {
        internal QuickpayException(string message, WebException innerException) : base(message, innerException)
        {
        }

        internal QuickpayException(string message) : base(message)
        {
        }
    }
}