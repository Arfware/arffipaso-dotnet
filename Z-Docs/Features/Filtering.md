# Filtering

You can use filtering a dataset with filter items. 
It is an extention method for IQueryable.
Filter method returns IQueryable.

Example:
```c#
var filters = new List<XFilterItem>(){
	new XFilterItem()
	{
		Key = "name",
		IsUsed = true,
		Type = ColumnTypes.String,
		Values = new List<object>(){"john"},
	}
};

var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();

matcheds.ForEach((s) =>
{
	System.Console.WriteLine($"Name Surname: {s.Name}");
});
```
