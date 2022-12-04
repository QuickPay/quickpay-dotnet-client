using System;
using System.Runtime.Serialization;

namespace QuickPay.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
	{
		public NotFoundException() : base()
		{
		}

        public NotFoundException(string msg) : base(msg)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

