using System;

namespace Quickpay
{
	public struct PageParameters
	{
		public PageParameters () : this (1, 20)
		{
		}

		public PageParameters (int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}

		public int Page { get; set; }

		public int PageSize { get; set; }
	}
}

