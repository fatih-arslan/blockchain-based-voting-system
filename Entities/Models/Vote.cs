using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ElectionId { get; set; }
        public DateTime VoteDate { get; set; }
    }
}
