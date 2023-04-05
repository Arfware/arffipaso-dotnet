# Sorting

You can sort a dataset with sort request item. 
It is an extention method for IQueryable.
Paginate method takes PageRequest and returns PageResponse.

Example:
```c#
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
```
