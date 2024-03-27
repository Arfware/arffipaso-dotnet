using System;
using System.Collections.Generic;

namespace Example.Models
{
	public class Hobby
	{
		public int Hid { get; set; }
		public string Name { get; set; }

		public List<Activity> Activities { get; set; }
	}
}
