using System;

namespace ArfFipaso.Pagination.Models
{
	public class XPageRequest
	{
		// Request
		public int CurrentPage { get; set; }
		public int PerPageCount { get; set; }
		public bool ListAll { get; set; }
	}

	public class XPageResponse : XPageRequest
	{
		// Response
		public int TotalPageCount { get; set; }
		public int TotalRowCount { get; set; }
		public bool HasNextPage { get; set; }
		public bool HasPreviousPage { get; set; }
	}
}