using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;


namespace PeopleManager.Data
{
	public class PeopleManagerDbContext: DbContext
	{
		public PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options): base(options)
		{
			
		}

		public DbSet<Person> People { get; set; }

		public void Seed()
		{
			if (!this.Database.IsInMemory())
			{
				return;
			}

			People.Add(new Person
			{
				Id = 1,
				Age = 39,
				Email = "bavo.ketels@vives.be",
				FirstName = "Bavo",
				LastName = "Ketels"
			});

			People.Add(new Person
			{
				Id = 2,
				Age = 55,
				Email = "thierry.malbrancke@vives.be",
				FirstName = "Thierry",
				LastName = "Malbrancke"
			});

			People.Add(new Person
			{
				Id = 3,
				Age = 20,
				Email = "killian.storm@vives.be",
				FirstName = "Killian",
				LastName = "Storm"
			});

			this.SaveChanges();
		}
	}
}
