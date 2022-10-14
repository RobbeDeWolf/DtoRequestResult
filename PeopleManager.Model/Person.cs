using System;
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Model
{
	public class Person
	{
		public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? Age { get; set; }
        public string Email { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
	}
}
