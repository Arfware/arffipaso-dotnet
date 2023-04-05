using System;

namespace ArfFipaso.Sorting.Models
{
	public class XSorting
	{
		public string Key { get; set; }
		public XSortingDirection Direction { get; set; }
	}

	public enum XSortingDirection
	{
		Ascending = 0,
		Descending = 1,
	}
}