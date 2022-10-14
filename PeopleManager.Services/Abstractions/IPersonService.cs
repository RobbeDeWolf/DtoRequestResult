using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;
using Vives.Services.Model;

namespace PeopleManager.Services.Abstractions
{
	public interface IPersonService
	{
		Task<PersonResult?> GetAsync(int id);
		Task<IList<PersonResult>> FindAsync();
        Task<ServiceResult<PersonResult>> CreateAsync(PersonRequest person);
        Task<PersonResult?> UpdateAsync(int id, PersonRequest person);
        Task<bool> DeleteAsync(int id);
	}
}
