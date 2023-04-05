namespace Example
{
	class Program
	{
		static void Main(string[] args)
		{
			RunFilterExamples();
			RunPaginationExamples();
			RunSortingExamples();
		}

		static void RunFilterExamples()
		{
			System.Console.WriteLine("Filtering Example\n");
			FilteringExamples.FilteringGuidFieldsInChildObject();
			FilteringExamples.FilteringDateTimeFields();
			FilteringExamples.FilteringDoubleNumberFields();
			FilteringExamples.FilteringGuidFields();
			FilteringExamples.FilteringGuidFieldsInChildObject();
			FilteringExamples.FilteringNumberRangeFields();
			FilteringExamples.FilteringStringFields();
			FilteringExamples.FilteringStringFieldsInChildObject();
			FilteringExamples.FilteringNumberFields();
			FilteringExamples.FilteringNumberInList();
			FilteringExamples.FilteringStringInList();
			FilteringExamples.OrTest();
		}

		static void RunPaginationExamples()
		{
			System.Console.WriteLine("Pagination Example");
			PaginationExamples.Paginate();
		}

		static void RunSortingExamples()
		{
			System.Console.WriteLine("Sorting Example");
			SortingExamples.Sort();
		}
	}
}
