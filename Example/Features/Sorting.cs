using System;
using System.Linq;
using ArfFipaso.Sorting.Models;
using ArfFipaso.Sorting.Extensions;

namespace Example
{
	public class SortingExamples
	{

		public static void Sort()
		{
			System.Console.WriteLine("\n---------------------------------------\nSort Example");

			var sorting = new XSorting()
			{
				Direction = XSortingDirection.Descending,
				Key = "Name",
			};

			var sortedList = StaticData.Students.AsQueryable().Sort(sorting).ToList();
			System.Console.WriteLine($"Sorted List Count: {sortedList.Count}");
			sortedList.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}
	}
}
