using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Services.Model.Results
{
    public class PersonResult
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        [Range(0, 99)]
        public int? Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
