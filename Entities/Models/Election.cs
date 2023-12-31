using Entities.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Models
{
    public class Election
    {        

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [LaterThan(nameof(StartDate), ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
        public List<Candidate> Candidates { get; set; }

        public Election()
        {
            Candidates = new List<Candidate>(); 
        }
    }
}
