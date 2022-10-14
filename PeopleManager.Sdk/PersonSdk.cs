using System.Net.Http.Json;
using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;
using Vives.Services.Model;

namespace PeopleManager.Sdk
{
    public class PersonSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IList<PersonResult>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();
            if (people is null)
            {
                return new List<PersonResult>();
            }
            return people;
        }
        
        public async Task<PersonResult?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<PersonResult>();
            
            return people;
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";

            var response = await httpClient.PostAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();

            var serviceResult = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
            if (serviceResult is null)
            {
                return new ServiceResult<PersonResult>
                {
                    Messages = new List<ServiceMessage>
                    {
                        new ServiceMessage
                        {
                            Code = "JsonNullError",
                            Message = "Could not parse json.",
                            Type = ServiceMessageType.Error
                        }
                    }
                };
            }

            return serviceResult;
        }

        public async Task<PersonResult?> Update(int id, PersonRequest person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.PutAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<PersonResult>();

            return people;
        }

        public async Task<bool> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<bool>();

            return people;
        }
    }
}