using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArfFipaso.Filter.Models;

namespace ArfFipaso.Filter.Utils
{
	public class ConversionUtils
	{
		internal static ConditionTypes GetConditionTypeFromString(string conditionAsString)
		{
			if (string.IsNullOrEmpty(conditionAsString) || string.IsNullOrWhiteSpace(conditionAsString))
				return ConditionTypes.Or;

			var clearConditionAsString = conditionAsString.ToLower().Replace(" ", "").Trim();

			switch (clearConditionAsString)
			{
				case "or":
					return ConditionTypes.Or;

				case "and":
					return ConditionTypes.And;

				default:
					return ConditionTypes.Unknown;
			}
		}

		internal static (List<object> TypedValues, object Min, object Max) CreateTypedValues(List<object> unTypedValues, object unTypedMin, object unTypedMax, string targetType)
		{
			var values = new List<object>();
			object minValue = null;
			object maxValue = null;

			if (targetType == ColumnTypes.Date || targetType == ColumnTypes.DateTime || targetType == ColumnTypes.NumberRange)
			{
				(var _, var min, var max) = GetValue(null, unTypedMin, unTypedMax, targetType);
				minValue = min;
				maxValue = max;
				return (values, minValue, maxValue);
			}

			foreach (var unTypedValue in unTypedValues)
			{
				(var value, _, _) = GetValue(unTypedValue, unTypedMin, unTypedMax, targetType);
				values.Add(value);
			}

			return (values, minValue, maxValue);
		}

		private static (object Value, object Min, object Max) GetValue(object unTypedValue, object unTypedMin, object unTypedMax, string targetType)
		{
			object value = null;
			object minValue = null;
			object maxValue = null;

			var shapedValue = Convert.ToString(unTypedValue);
			var shapedMinValue = Convert.ToString(unTypedMin);
			var shapedMaxValue = Convert.ToString(unTypedMax);

			switch (targetType)
			{
				case ColumnTypes.String:
					var stringValue = Convert.ToString(shapedValue);
					value = stringValue;
					break;

				case ColumnTypes.Guid:
					var guidValue = Guid.Parse(shapedValue.ToString());
					value = guidValue;
					break;

				case ColumnTypes.Number:
					var intValue = Convert.ToInt32(shapedValue);
					value = intValue;
					break;

				case ColumnTypes.DoubleNumber:
					var doubleValue = Convert.ToDouble(shapedValue);
					value = doubleValue;
					break;

				case ColumnTypes.Boolean:
					var boolValue = Convert.ToBoolean(shapedValue);
					value = boolValue;
					break;

				case ColumnTypes.Enum:
					var enumValue = Convert.ToInt32(shapedValue);
					value = enumValue;
					break;

				case ColumnTypes.NumberRange:
					var min = Convert.ToInt32(shapedMinValue);
					var max = Convert.ToInt32(shapedMaxValue);
					minValue = min;
					maxValue = max;
					break;

				case ColumnTypes.Date:
				case ColumnTypes.DateTime:
					var minDate = DateTime.Parse(shapedMinValue);
					var maxDate = DateTime.Parse(shapedMaxValue);

					minDate = minDate.AddSeconds(-1 * minDate.Second);
					minDate = minDate.AddMinutes(-1 * minDate.Minute);
					minDate = minDate.AddHours(-1 * minDate.Hour);

					maxDate = maxDate.AddSeconds(59 - maxDate.Second);
					maxDate = maxDate.AddMinutes(59 - maxDate.Minute);
					maxDate = maxDate.AddHours(23 - maxDate.Hour);

					minValue = minDate;
					maxValue = maxDate;
					break;

				default:
					System.Console.WriteLine($"UnHandled Filter Item Type: {targetType}");
					break;
			}

			return (value, minValue, maxValue);
		}
	}
}