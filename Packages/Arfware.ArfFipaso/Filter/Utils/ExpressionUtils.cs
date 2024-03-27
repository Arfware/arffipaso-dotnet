using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ArfFipaso.Filter.Constants;
using ArfFipaso.Filter.Models;

namespace ArfFipaso.Filter.Utils
{
	public class ExpressionUtils
	{
		internal static Expression GetSafeMemberExpression(MemberExpression memberExpression)
		{
			if (memberExpression == null)
				return null;

			// Type Correction for Nullable Types
			Expression safeMemberExpression = memberExpression;
			if (Nullable.GetUnderlyingType(memberExpression.Type) != null)
			{
				safeMemberExpression = Expression.Convert(memberExpression, Nullable.GetUnderlyingType(memberExpression.Type));
			}

			return safeMemberExpression;
		}

		internal static LambdaExpression GetLambdaWithConstantEqual2(ParameterExpression parameterExpression, Expression memberExpression, object value)
		{
			if (memberExpression.Type.BaseType == typeof(Enum))
				value = Enum.ToObject(memberExpression.Type, value);

			// Inner Lambda
			ConstantExpression valExpression = Expression.Constant(value, memberExpression.Type);
			BinaryExpression binaryExpression = Expression.Equal(memberExpression, valExpression);

			var innnerLambda = Expression.Lambda(binaryExpression, parameterExpression);

			return innnerLambda;
		}

		internal static LambdaExpression GetLambdaWithConstantEqual(ParameterExpression parameterExpression, Expression memberExpression, List<object> values, string conditionTypeAsString)
		{
			BinaryExpression binaryExpression = null;

			for (int i = 0; i < values.Count; i++)
			{
				var value = values[i];

				if (memberExpression.Type.BaseType == typeof(Enum))
					value = Enum.ToObject(memberExpression.Type, value);

				// Inner Lambda
				ConstantExpression valExpression = Expression.Constant(value, memberExpression.Type);
				BinaryExpression tempBinaryExpression = Expression.Equal(memberExpression, valExpression);

				if (binaryExpression != null)
				{
					var conditionType = ConversionUtils.GetConditionTypeFromString(conditionTypeAsString);
					switch (conditionType)
					{
						case ConditionTypes.Or:
							binaryExpression = Expression.Or(binaryExpression, tempBinaryExpression);
							break;

						case ConditionTypes.And:
							binaryExpression = Expression.AndAlso(binaryExpression, tempBinaryExpression);
							break;

						default: // ConditionTypes.NoCondition:
							binaryExpression = Expression.Or(binaryExpression, tempBinaryExpression);
							break;
					}
				}
				else
				{
					binaryExpression = tempBinaryExpression;
				}
			}

			var innnerLambda = Expression.Lambda(binaryExpression, parameterExpression);

			return innnerLambda;
		}

		internal static LambdaExpression GetLambdaWithConstantContains(ParameterExpression parameterExpression, Expression memberExpression, object value)
		{
			ConstantExpression valExpression = Expression.Constant(value.ToString().Trim().ToLower(), typeof(string));
			MethodInfo stringToLowerMethod = ReflectionUtils.GetStringToLowerMethod();
			MethodInfo stringContainsMethod = ReflectionUtils.GetStringContainsMethod();
			var toLowerMethodExpr = Expression.Call(memberExpression, stringToLowerMethod);
			var containsMethodExpr = Expression.Call(toLowerMethodExpr, stringContainsMethod, valExpression);

			var lambda = Expression.Lambda(containsMethodExpr, parameterExpression);
			return lambda;
		}

		internal static LambdaExpression GetLambdaWithConstantRange(ParameterExpression parameterExpression, Expression memberExpression, object minValue, object maxValue)
		{
			var minExpression = Expression.GreaterThanOrEqual(memberExpression, Expression.Constant(minValue, minValue.GetType()));
			var maxExpression = Expression.LessThanOrEqual(memberExpression, Expression.Constant(maxValue, maxValue.GetType()));
			var and = Expression.AndAlso(minExpression, maxExpression);

			var lambda = Expression.Lambda(and, parameterExpression);
			return lambda;
		}

		internal static Expression GetListLambda2(ParameterExpression baseParameterExpression, Expression baseMemberExpression, ParameterExpression listParameterExpression, Expression lambda)
		{
			MethodInfo anyMethod = ReflectionUtils.GetGenericAnyMethod(listParameterExpression.Type);
			var callExpression = Expression.Call(anyMethod, baseMemberExpression, lambda); // memberExpr
			var outherLambda = Expression.Lambda(callExpression, baseParameterExpression);
			return outherLambda;
		}

		internal static Expression GetListLambda(List<Expression> parameterExpressions, List<Expression> memberExpressions, LambdaExpression lambda)
		{
			LambdaExpression lambdaExpression = lambda;

			for (int i = parameterExpressions.Count - 2; i >= 0; i--)
			{
				var memberExpression = memberExpressions[i];
				var parameterExpression = parameterExpressions[i];
				var lastParameterExpression = parameterExpressions[i + 1];

				MethodInfo anyMethod = ReflectionUtils.GetGenericAnyMethod(lastParameterExpression.Type);
				var callExpression = Expression.Call(anyMethod, memberExpression, lambdaExpression); // memberExpr
				lambdaExpression = Expression.Lambda(callExpression, (ParameterExpression)parameterExpression);
			}
			return lambdaExpression;
		}

		internal static (ParameterExpression BaseParameterExpression, Expression BaseMemberExpression, ParameterExpression ListParameterExpression, Expression ListMemberExpression) GetExpressions2(Type baseObjectType, string propName)
		{
			ParameterExpression baseParameterExpression = Expression.Parameter(baseObjectType, "x");
			MemberExpression baseMemberExpression = null;

			ParameterExpression listParameterExpression = null; // (i=> i....)
			MemberExpression listMemberExpression = null;

			var propertyParts = propName.Split(FilterConstants.ProperyDelimiter);
			var flagListPassed = false;
			for (int i = 0; i < propertyParts.Length; i++)
			{
				var propertyPart = propertyParts[i];

				if (!flagListPassed)
				{
					if (baseMemberExpression == null)
						baseMemberExpression = Expression.Property(baseParameterExpression, propertyPart);
					else
						baseMemberExpression = Expression.Property(baseMemberExpression, propertyPart);
				}
				else
				{
					if (listMemberExpression == null)
						listMemberExpression = Expression.Property(listParameterExpression, propertyPart);
					else
						listMemberExpression = Expression.Property(listMemberExpression, propertyPart);
				}

				if (!flagListPassed && Nullable.GetUnderlyingType(baseMemberExpression.Type) == null && baseMemberExpression.Type.GetGenericArguments().Length > 0)
				{
					var type = baseMemberExpression.Type.GetGenericArguments()[0];
					listParameterExpression = Expression.Parameter(type, "i");

					flagListPassed = true;
				}
			}

			var safeBaseMemberExpression = GetSafeMemberExpression(baseMemberExpression);
			var safeListMemberExpression = GetSafeMemberExpression(listMemberExpression);

			return (baseParameterExpression, safeBaseMemberExpression, listParameterExpression, safeListMemberExpression);
		}


		internal static (List<Expression> ParameterExpressions, List<Expression> MemberExpressions) GetExpressions(Type baseObjectType, string propName)
		{
			ParameterExpression baseParameterExpression = Expression.Parameter(baseObjectType, "x");
			MemberExpression baseMemberExpression = null;

			List<Expression> parameterExpressions = new List<Expression>();
			List<Expression> memberExpressions = new List<Expression>();

			var propertyParts = propName.Split(FilterConstants.ProperyDelimiter);
			for (int i = 0; i < propertyParts.Length; i++)
			{
				var propertyPart = propertyParts[i];

				if (baseMemberExpression == null)
					baseMemberExpression = Expression.Property(baseParameterExpression, propertyPart);
				else
					baseMemberExpression = Expression.Property(baseMemberExpression, propertyPart);

				if ((i == propertyParts.Length - 1) || (Nullable.GetUnderlyingType(baseMemberExpression.Type) == null && baseMemberExpression.Type.GetGenericArguments().Length > 0))
				{
					var safeBaseMemberExpression = GetSafeMemberExpression(baseMemberExpression);
					parameterExpressions.Add(baseParameterExpression);
					memberExpressions.Add(safeBaseMemberExpression);

					if (i != propertyParts.Length - 1)
					{
						// Create new list expression
						var type = baseMemberExpression.Type.GetGenericArguments()[0];
						var paramChar = Convert.ToChar(97 + i) + ""; // a,b,c,d,e,...
						baseParameterExpression = Expression.Parameter(type, paramChar);
						baseMemberExpression = null;
					}
				}
			}

			return (parameterExpressions, memberExpressions);
		}
	}
}
