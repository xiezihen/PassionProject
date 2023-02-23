using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class ProblemHoldsViewModel
    {
        public Problem Problem { get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}