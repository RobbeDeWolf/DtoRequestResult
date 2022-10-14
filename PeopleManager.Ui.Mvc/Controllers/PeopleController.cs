using Microsoft.AspNetCore.Mvc;
using PeopleManager.Sdk;
using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.Controllers
{
	public class PeopleController : Controller
	{
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
		{
            _personSdk = personSdk;
		}

		public async Task<IActionResult> Index()
		{
			var people = await _personSdk.Find();
			return View(people);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(PersonRequest person)
		{
			if (!ModelState.IsValid)
            {
                return ShowCreateView(person);
            }

			var serviceResult = await _personSdk.Create(person);
            if (!serviceResult.IsSuccess)
            {
                var errors = serviceResult
                    .Messages
                    .Where(m => m.Type == ServiceMessageType.Error)
                    .ToList();
                foreach (var error in errors)
                {
					ModelState.AddModelError("", error.Message);
                }

                return ShowCreateView(person);
            }

			return RedirectToAction("Index");
		}

        private IActionResult ShowCreateView(PersonRequest request)
        {
            var personResult = new PersonResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email
            };

            return View("Create", personResult);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var person = await _personSdk.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, PersonRequest person)
		{
			if (!ModelState.IsValid)
            {
                var personResult = await _personSdk.Get(id);
                if (personResult is null)
                {
                    return RedirectToAction("Index");
                }
                personResult.FirstName = person.FirstName;
                personResult.LastName = person.LastName;
                personResult.Age = person.Age;
                personResult.Email = person.Email;

				return View(personResult);
			}

			await _personSdk.Update(id, person);
			
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var person = await _personSdk.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		[Route("People/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _personSdk.Delete(id);

			return RedirectToAction("Index");
		}
	}
}
