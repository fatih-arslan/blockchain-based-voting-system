using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class VoteVM
    {
        public Election Election { get; set; }
        public bool UserAlreadyVoted { get; set; }
    }
}
