using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entities.Models
{
    public class Candidate
    {
        public Candidate()
        {
            Votes = new List<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Surname is a required field.")]
        public string Surname{ get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        public string Description { get; set; }
        public int ElectionId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public List<Vote> Votes { get; set; }

        [NotMapped]
        public int VoteCount => Votes.Count;        
    }
}
