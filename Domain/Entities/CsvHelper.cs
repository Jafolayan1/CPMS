﻿using CsvHelper.Configuration.Attributes;

namespace Domain.Entities
{
	public class Lecturers
	{
		[Index(0)]
		public string SerialNo { get; set; } = "";

		[Index(1)]
		public string FullName { get; set; } = "";

		[Index(2)]
		public string FileNo { get; set; } = "";

		[Index(3)]
		public string Department { get; set; } = "";

		[Index(4)]
		public string PhoneNo { get; set; } = "";

		[Index(5)]
		public string Email { get; set; } = "";
	}

	public class Students
	{
		[Index(0)]
		public string SerialNo { get; set; } = "";

		[Index(1)]
		public string MatricNo { get; set; } = "";

		[Index(2)]
		public string Names { get; set; } = "";

		[Index(3)]
		public string Level { get; set; } = "";

		[Index(4)]
		public string Department { get; set; } = "";
	}
}