using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace MyAPI.Models
{
    public class UserDto : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        
        //Put Business validations here
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = new List<ValidationResult>();
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("User Name caanot be Test", new[] { nameof(UserName) });

            if(Password.Equals("123456"))
                yield return new ValidationResult("Password cannot be 123456", new[] { nameof(Password) });

            if(Gender == GenderType.Male && Age > 30)
                yield return new ValidationResult("Men over 30 years are not accepted", new[] { nameof(Gender) , nameof(Age)});
        }
    }
}

