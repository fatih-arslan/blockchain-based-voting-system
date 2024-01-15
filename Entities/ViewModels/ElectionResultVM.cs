using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ElectionResultVM
    {
        public string ElectionName { get; set; }
        public List<Candidate> Candidates { get; set; }
    }
}
