using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace MyAPI.Models
{
    public class UserDto
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
    }
}

