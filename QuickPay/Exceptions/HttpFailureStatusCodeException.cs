using System;
using System.Runtime.Serialization;

namespace QuickPay.Exceptions
{
    [Serializable]
    public class HttpFailureStatusCodeException : Exception
	{
		public int StatusCode { get; set; }

        public HttpFailureStatusCodeException(int statusCode) : base()
		{
			this.StatusCode = statusCode;
		}

        public HttpFailureStatusCodeException(int statusCode, String msg) : base(msg)
        {
            this.StatusCode = statusCode;
        }

        protected HttpFailureStatusCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}