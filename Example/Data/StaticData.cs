using System;
using System.Collections.Generic;
using Example.Models;

namespace Example
{
	public class StaticData
	{
		public static List<Student> Students = new List<Student>(){
			new Student()
			{
				Id = 1,
				Name="John Doe",
				Weight=75.3,
				BirthDate = DateTime.Parse("1995.05.23 12:15:00"),
				ParentId=Guid.Parse("c8ef0fa8-5939-4d4e-874b-c2ffed6656a9"),
				HomeAddress = new Address()
				{
					CountryId=Guid.Parse("aeee08d7-bce4-448b-b960-fd5888a291d4"),
					City = "NYC",
					PlainAddress="LittleTown Block, No:213, NYC, USA"
				},
				Other = new OtherInformation()
				{
					Hobbies = new List<Hobby>()
					{
						new Hobby()
						{
							Hid=1,
							Name="Sailing"
						},
						new Hobby()
						{
							Hid=2,
							Name="Table Games"
						}
					},
				}
			},
			new Student()
			{
				Id = 2,
				Name="Marry Gleen",
				Weight=70.2,
				BirthDate = DateTime.Parse("1995.05.24 23:59:00"),
				ParentId=Guid.Parse("c329cf62-267e-4e78-ab37-ed505099ded9"),
				HomeAddress = new Address()
				{
					CountryId=Guid.Parse("1ac36db6-9b8a-4a81-8057-f220f5a91130"),
					City = "San Fransisco",
					PlainAddress="MountainLake, No:114, CA, USA"
				},
				Other = new OtherInformation()
				{
					Hobbies = new List<Hobby>()
					{
						new Hobby()
						{
							Hid=3,
							Name="Swimming"
						},
						new Hobby()
						{
							Hid=4,
							Name="Tennis"
						}
					},
				}
			}
		};
	}
}
