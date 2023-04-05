using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ArfFipaso.Filter.Models;
using ArfFipaso.Pagination.Models;

namespace ArfFipaso.Pagination.Extensions
{
	public static class AutomatedQueryExtensions
	{
		public static int DefaultPerPage = 20;

		public static IQueryable<T> Paginate<T>(this IQueryable<T> originalList, XPageRequest page) where T : class
		{
			if (page == null)
				return originalList.Take(DefaultPerPage);

			if (page.ListAll)
				return originalList;

			var skipCount = page.PerPageCount * (page.CurrentPage - 1);
			return originalList.Skip(skipCount).Take(page.PerPageCount);
		}

		public static XPageResponse GetPage<T>(this IQueryable<T> originalList, XPageRequest page) where T : class
		{
			var totalRowCount = originalList.Count();

			if (page == null)
			{
				page = new XPageRequest()
				{
					PerPageCount = DefaultPerPage,
					CurrentPage = 1,
				};
			}

			var totalPageCount = totalRowCount / page.PerPageCount;
			if ((totalPageCount * page.PerPageCount) < totalRowCount)
				totalPageCount++;

			return new XPageResponse()
			{
				PerPageCount = page.PerPageCount,
				CurrentPage = page.ListAll ? -1 : page.CurrentPage,
				TotalRowCount = totalRowCount,
				TotalPageCount = totalPageCount,
				HasNextPage = page.ListAll ? false : page.CurrentPage < totalPageCount,
				HasPreviousPage = page.ListAll ? false : page.CurrentPage > 1,
				ListAll = page.ListAll,
			};
		}
	}
}