using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Votes = new List<Vote>();
        }
        [Required(ErrorMessage = "Identification number is a required field.")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is a required field.")]
        public string Surname { get; set; }
        public DateTime RegistrationDate { get; set; }

        [InverseProperty("User")]
        public List<Vote> Votes { get; set; }
    }
}
