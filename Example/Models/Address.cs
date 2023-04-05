using System;

namespace Example.Models
{
	public class Address
	{
		public string City { get; set; }
		public string PlainAddress { get; set; }
		public Guid CountryId { get; set; }
	}
}