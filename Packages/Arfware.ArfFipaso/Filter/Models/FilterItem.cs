using System;
using System.Collections.Generic;

namespace ArfFipaso.Filter.Models
{
	public class XFilterItem
	{
		// Meta
		public string Key { get; set; }
		public string Type { get; set; }
		public bool IsUsed { get; set; }

		// Values
		public List<object> Values { get; set; }
		public object Min { get; set; }
		public object Max { get; set; }

		// Condition
		public string ConditionType { get; set; }
	}
}