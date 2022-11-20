using System;
namespace QuickPay.IntegrationTests.Exceptions
{
	public class ApiKeyNotSpecifiedException : Exception
	{
		public ApiKeyNotSpecifiedException() : base()
		{
		}

        public ApiKeyNotSpecifiedException(string msg) : base(msg)
        {
        }
    }
}