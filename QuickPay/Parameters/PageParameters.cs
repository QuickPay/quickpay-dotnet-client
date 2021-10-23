namespace Quickpay
{
	public struct PageParameters
	{
		public PageParameters (int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}

		public int Page { get; set; }

		public int PageSize { get; set; }
	}
}

