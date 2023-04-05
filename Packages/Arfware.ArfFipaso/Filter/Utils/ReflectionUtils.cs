using System;
using System.Linq;
using System.Reflection;

namespace ArfFipaso.Filter.Utils
{
	public class ReflectionUtils
	{
		internal static MethodInfo GetGenericAnyMethod(Type type)
		{
			MethodInfo anyMethod = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
															.First(m => m.Name == "Any" && m.GetParameters().Count() == 2);
			anyMethod = anyMethod.MakeGenericMethod(type);

			return anyMethod;
		}

		internal static MethodInfo GetStringToLowerMethod()
		{
			MethodInfo stringToLowerMethod = typeof(String).GetMethods().FirstOrDefault(f => f.Name == "ToLower");
			return stringToLowerMethod;
		}

		internal static MethodInfo GetStringContainsMethod()
		{
			MethodInfo stringContainsMethod = typeof(String).GetMethod("Contains", new[] { typeof(string) });
			return stringContainsMethod;
		}
	}
}