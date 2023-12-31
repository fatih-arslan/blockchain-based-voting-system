using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="Identification number is a required field.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage ="Password is a required field.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is a required field.")]
        public string Surname { get; set; }
        public string? EthereumAddress { get; set; }                
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }

    }
}
