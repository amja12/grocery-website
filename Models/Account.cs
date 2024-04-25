using System.ComponentModel.DataAnnotations;

namespace coreFormsAndValidations.models

{
    public class Account
    {   
        [Key]
        [Required(ErrorMessage ="username is required")]
        [MinLength(5,ErrorMessage ="minimum length of username should be 5")]
        [MaxLength(15,ErrorMessage ="max length of username should be 15")]
        public string? Username{get; set;}
        [Required]
        [MinLength(8,ErrorMessage ="minimum length of PASSWORD should be 8")]
        [MaxLength(15,ErrorMessage ="max length of PASSWORD should be 15")]
        public string? Password{get; set;}
        [Required]
        [MinLength(1,ErrorMessage ="minimum length of NAME should be 1")]
        [MaxLength(20,ErrorMessage ="max length of NAME should be 20")]
        public string? Name{get; set;}
        [Required]
        [MinLength(3,ErrorMessage ="minimum length of Address should be 3")]
        [MaxLength(20,ErrorMessage ="max length of Address should be 20")]
        public string? Address{get; set;}
        [Required]
        [EmailAddress(ErrorMessage ="enter email correctly")]       
        public string? Email{get; set;}
        [Required]
        [Phone(ErrorMessage ="enter phone number")]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string? Phone{get; set;}

    }

}