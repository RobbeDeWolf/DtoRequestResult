using Microsoft.EntityFrameworkCore;
using PeopleManager.Data;
using PeopleManager.Model;
using PeopleManager.Services.Abstractions;
using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;
using Vives.Services.Model;

namespace PeopleManager.Services
{
	public class PersonService : IPersonService
	{
		private readonly PeopleManagerDbContext _database;

		public PersonService(PeopleManagerDbContext database)
		{
			_database = database;
		}

		public async Task<PersonResult?> GetAsync(int id)
		{
			var person = await _database.People
                .AsNoTracking()
                .Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Email = p.Email
                })
                .SingleOrDefaultAsync(p => p.Id == id);

            return person;
        }

		public async Task<IList<PersonResult>> FindAsync()
		{
			var people = await _database.People
                .AsNoTracking()
                .Select(p => new PersonResult
                {
					Id = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Age = p.Age,
					Email = p.Email
                })
				.ToListAsync();

			return people;
		}


		public async Task<ServiceResult<PersonResult>> CreateAsync(PersonRequest request)
        {
			//Validate
            if (request.FirstName == "John")
            {
                var serviceResult = new ServiceResult<PersonResult>();
                serviceResult.Messages.Add(new ServiceMessage

                {
                    Code = "NotJohn",
                    Message = "First name cannot be John. How dare you!",
                    Type = ServiceMessageType.Error
                });
                return serviceResult;
            }


            var person = new Person
            {
				FirstName = request.FirstName,
				LastName = request.LastName,
				Age = request.Age,
				Email = request.Email,
                CreatedDate = DateTime.UtcNow
            };

            _database.People.Add(person);
			await _database.SaveChangesAsync();

			var personResult = await GetAsync(person.Id);
            if (personResult is null)
            {
                var serviceResult = new ServiceResult<PersonResult>();
				serviceResult.Messages.Add(new ServiceMessage

                {
					Code = "NotFound",
					Message = "Person not found after create",
					Type = ServiceMessageType.Error
                });
                return serviceResult;
            }

            var successServiceResult = new ServiceResult<PersonResult>(personResult);

            if (person.FirstName == "Bavo")
            {
                successServiceResult.Messages.Add(new ServiceMessage
                {
                    Code = "BavoDetected",
                    Message = "Greetings, my liege...",
                    Type = ServiceMessageType.Info
                });
            }

            return successServiceResult;
        }

		public async Task<PersonResult?> UpdateAsync(int id, PersonRequest person)
        {
            var dbPerson = await _database.People.SingleOrDefaultAsync(p => p.Id == id);

			if (dbPerson is null)
			{
				return null;
			}

			dbPerson.FirstName = person.FirstName;
			dbPerson.LastName = person.LastName;
			dbPerson.Email = person.Email;
			dbPerson.Age = person.Age;

			await _database.SaveChangesAsync();

			return await GetAsync(id);
		}

		public async Task<bool> DeleteAsync(int id)
        {
            var person = new Person { Id = id };
            _database.People.Attach(person);

			_database.People.Remove(person);
			await _database.SaveChangesAsync();

			return true;
		}
	}
}
