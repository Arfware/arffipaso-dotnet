using System;
using System.Linq;
using System.Collections.Generic;
using ArfFipaso.Filter.Models;
using ArfFipaso.Filter.Extensions;

namespace Example
{
	public class FilteringExamples
	{

		public static void FilteringStringFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringStringFields");

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
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringStringFieldsInChildObject()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringStringFieldsInChildObject");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "homeAddress_City",
					IsUsed = true,
					Type = ColumnTypes.String,
					Values = new List<object>(){"NYC"},
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringDateTimeFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringDateTimeFields");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "birthDate",
					IsUsed = true,
					Type = ColumnTypes.Date,
					Min = "1995.05.23",
					Max = "1995.05.23"
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringDoubleNumberFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringDoubleNumberFields");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "weight",
					IsUsed = true,
					Type = ColumnTypes.DoubleNumber,
					Values = new List<object>(){"70.2"}
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringNumberRangeFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringNumberRangeFields");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "id",
					IsUsed = true,
					Type = ColumnTypes.NumberRange,
					Min = "1",
					Max = "1"
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringGuidFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringGuidFields");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "parentId",
					IsUsed = true,
					Type = ColumnTypes.Guid,
					Values =new List<object>(){ "c8ef0fa8-5939-4d4e-874b-c2ffed6656a9"},
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringGuidFieldsInChildObject()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringGuidFieldsInChildObject");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "homeAddress_CountryId",
					IsUsed = true,
					Type = ColumnTypes.Guid,
					Values = new List<object>(){"1ac36db6-9b8a-4a81-8057-f220f5a91130"},
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringNumberFields()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringNumberFields");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "id",
					IsUsed = true,
					Type = ColumnTypes.Number,
					Values = new List<object>(){"2"},
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringNumberInList()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringNumberInList");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "other_Hobbies_Hid",
					IsUsed = true,
					Type = ColumnTypes.Number,
					Values = new List<object>(){"3"},
				},
				new XFilterItem()
				{
					Key = "other_Hobbies_Hid",
					IsUsed = true,
					Type = ColumnTypes.Number,
					Values = new List<object>(){"4"},
				}
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}

		public static void FilteringStringInList()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringStringInList");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "other_Hobbies_Name",
					IsUsed = true,
					Type = ColumnTypes.String,
					Values = new List<object>(){ "tennis"} ,
				},
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}


		public static void OrTest()
		{
			System.Console.WriteLine("\n---------------------------------------\n FilteringNumberInList");

			var filters = new List<XFilterItem>(){
				new XFilterItem()
				{
					Key = "id",
					IsUsed = true,
					Type = ColumnTypes.Number,
					Values = new List<object>(){"1", "2"},
					ConditionType="or",
				},
			};

			var matcheds = StaticData.Students.AsQueryable().Filter(filters).ToList();
			System.Console.WriteLine($"Matched Count: {matcheds.Count}");
			matcheds.ForEach((s) =>
			{
				System.Console.WriteLine($"Name Surname: {s.Name}");
			});
		}
	}
}
