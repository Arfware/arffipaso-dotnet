using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ArfFipaso.Sorting.Models;

namespace ArfFipaso.Sorting.Extensions
{
	public static class SortingExtensions
	{
		public static char ProperyDelimiter = '_';

		public static IQueryable<T> Sort<T>(this IQueryable<T> query, XSorting sorting) where T : class
		{
			if (sorting == null)
			{
				var type = typeof(T);
				var property = type.GetProperty("CreatedAt");

				if (property == null)
					return query;

				sorting = new XSorting()
				{
					Key = "CreatedAt",
					Direction = XSortingDirection.Descending,
				};
			}

			var normalizedKey = char.ToUpper(sorting.Key[0]) + sorting.Key.Substring(1);

			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
			MemberExpression memberExpression = null;

			var propertyParts = normalizedKey.Split(ProperyDelimiter);
			for (int i = 0; i < propertyParts.Length; i++)
			{
				var propertyPart = propertyParts[i];

				if (i == 0)
					memberExpression = Expression.Property(parameterExpression, propertyPart);
				else
					memberExpression = Expression.Property(memberExpression, propertyPart);
			}

			var lambda = Expression.Lambda(memberExpression, parameterExpression);

			var direction = sorting.Direction == XSortingDirection.Ascending ? "OrderBy" : "OrderByDescending";

			MethodCallExpression resultExp = Expression.Call(typeof(Queryable), direction, new Type[] { parameterExpression.Type, memberExpression.Type }, query.Expression, Expression.Quote(lambda));
			return query.Provider.CreateQuery<T>(resultExp);
		}

		// This works like Above Function
		// public static IQueryable<T> Sort2<T>(this IQueryable<T> query, XSorting sorting) where T : class
		// {
		// 	if (sorting == null)
		// 		return query;

		// 	var normalizedKey = char.ToUpper(sorting.Key[0]) + sorting.Key.Substring(1);

		// 	var type = typeof(T);
		// 	var property = type.GetProperty(normalizedKey);

		// 	ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
		// 	var propertyAccess = Expression.MakeMemberAccess(parameterExpression, property);
		// 	var orderByExp = Expression.Lambda(propertyAccess, parameterExpression);

		// 	var direction = sorting.Direction == XSortingDirection.Ascending ? "OrderBy" : "OrderByDescending";

		// 	MethodCallExpression resultExp = Expression.Call(typeof(Queryable), direction, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
		// 	return query.Provider.CreateQuery<T>(resultExp);
		// }
	}
}