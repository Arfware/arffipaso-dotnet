using System;
using System.Collections.Generic;

namespace Example.Models
{
	public class Student
	{
		public int? Id { get; set; }
		public Guid ParentId { get; set; }
		public string Name { get; set; }
		public double Weight { get; set; }
		public DateTime BirthDate { get; set; }
		public Address HomeAddress { get; set; }
		public Address TemporaryAddress { get; set; }

		public OtherInformation Other { get; set; }
	}
}
