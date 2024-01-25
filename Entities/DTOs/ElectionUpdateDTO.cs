using Entities.Attributes;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ElectionUpdateDTO
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
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End Date must be later than Start Date.")]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
        public List<Candidate> Candidates { get; set; }

        public ElectionUpdateDTO()
        {
            Candidates = new();
        }
    }
}
