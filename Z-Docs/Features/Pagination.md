# Pagination

You can paginate a dataset with pagination request item. 
It is an extention method for IQueryable.
Paginate method takes PageRequest and returns PageResponse.

Example:
```c#
var pageRequest = new XPageRequest()
{
	CurrentPage = 0,
	ListAll = false,
	PerPageCount = 1,
};

var pageResponse = new XPageResponse()
{
	HasNextPage = true,
};

var query = StaticData.Students.AsQueryable();

while (pageResponse.HasNextPage)
{
	pageRequest.CurrentPage++;
	pageResponse = query.GetPage(pageRequest);

	var matcheds = query.Paginate(pageRequest).ToList();
	System.Console.WriteLine($"Matched Count: {matcheds.Count}");
	
	matcheds.ForEach((s) =>
	{
		System.Console.WriteLine($"Page:{pageResponse.CurrentPage}, Name Surname: {s.Name}");
	});
}
```
