using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ArfFipaso.Filter.Models;
using ArfFipaso.Filter.Utils;

namespace ArfFipaso.Filter.Extensions
{
	public static class FilterExtensions
	{

		public static IQueryable<T> Filter2<T>(this IQueryable<T> query, List<XFilterItem> filterItems) where T : class
		{
			if (filterItems == null)
				return query;

			var usedFilterItems = filterItems.Where(item => item.IsUsed).ToList();

			foreach (var filterItem in usedFilterItems)
			{
				var (baseParameterExpression, baseMemberExpression, listParameterExpression, listMemberExpression) = ExpressionUtils.GetExpressions2(typeof(T), filterItem.Key);
				ParameterExpression parameterExpression = listParameterExpression ?? baseParameterExpression;
				Expression memberExpression = listMemberExpression ?? baseMemberExpression;
				Expression lambda = null;

				(var values, var min, var max) = ConversionUtils.CreateTypedValues(filterItem.Values, filterItem.Min, filterItem.Max, filterItem.Type);

				switch (filterItem.Type)
				{
					case ColumnTypes.String:
						var stringValue = Convert.ToString(filterItem.Values?[0] ?? "");
						lambda = ExpressionUtils.GetLambdaWithConstantContains(parameterExpression, memberExpression, stringValue);
						break;

					case ColumnTypes.Guid:
					case ColumnTypes.Number:
					case ColumnTypes.DoubleNumber:
					case ColumnTypes.Boolean:
					case ColumnTypes.Enum:
						lambda = ExpressionUtils.GetLambdaWithConstantEqual(parameterExpression, memberExpression, values, filterItem.ConditionType);
						break;

					case ColumnTypes.NumberRange:
					case ColumnTypes.Date:
					case ColumnTypes.DateTime:
						lambda = ExpressionUtils.GetLambdaWithConstantRange(parameterExpression, memberExpression, min, max);
						break;

					default:
						System.Console.WriteLine($"UnHandled Filter Item Type: {filterItem.Type}");
						break;
				}

				// Non-List Operations
				if (listParameterExpression == null)
				{
					query = query.Where((Expression<Func<T, bool>>)lambda);
				}
				// List Operations
				else
				{
					var outherLambda = ExpressionUtils.GetListLambda2(baseParameterExpression, baseMemberExpression, listParameterExpression, lambda);
					query = query.Where((Expression<Func<T, bool>>)outherLambda);
				}
			}

			return query;
		}

		public static IQueryable<T> Filter<T>(this IQueryable<T> query, List<XFilterItem> filterItems) where T : class
		{
			if (filterItems == null)
				return query;

			var usedFilterItems = filterItems.Where(item => item.IsUsed).ToList();

			foreach (var filterItem in usedFilterItems)
			{
				var (parameterExpressions, memberExpressions) = ExpressionUtils.GetExpressions(typeof(T), filterItem.Key);
				ParameterExpression parameterExpression = (ParameterExpression)parameterExpressions[parameterExpressions.Count - 1];
				Expression memberExpression = memberExpressions[memberExpressions.Count - 1];
				LambdaExpression lambda = null;

				(var values, var min, var max) = ConversionUtils.CreateTypedValues(filterItem.Values, filterItem.Min, filterItem.Max, filterItem.Type);

				switch (filterItem.Type)
				{
					case ColumnTypes.String:
						var stringValue = Convert.ToString(filterItem.Values?[0] ?? "");
						lambda = ExpressionUtils.GetLambdaWithConstantContains(parameterExpression, memberExpression, stringValue);
						break;

					case ColumnTypes.Guid:
					case ColumnTypes.Number:
					case ColumnTypes.DoubleNumber:
					case ColumnTypes.Boolean:
					case ColumnTypes.Enum:
						lambda = ExpressionUtils.GetLambdaWithConstantEqual(parameterExpression, memberExpression, values, filterItem.ConditionType);
						break;

					case ColumnTypes.NumberRange:
					case ColumnTypes.Date:
					case ColumnTypes.DateTime:
						lambda = ExpressionUtils.GetLambdaWithConstantRange(parameterExpression, memberExpression, min, max);
						break;

					default:
						System.Console.WriteLine($"UnHandled Filter Item Type: {filterItem.Type}");
						break;
				}

				// Non-List Operations
				if (memberExpressions.Count == 1)
				{
					query = query.Where((Expression<Func<T, bool>>)lambda);
				}
				// List Operations
				else
				{
					var outherLambda = ExpressionUtils.GetListLambda(parameterExpressions, memberExpressions, lambda);
					query = query.Where((Expression<Func<T, bool>>)outherLambda);
				}
			}

			return query;
		}
	}
}