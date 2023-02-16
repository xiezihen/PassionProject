using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class Problem
    {
        [Key]
        public int ProblemID { get; set; }
        public string ProblemName { get; set; }
        public string ProblemGrade { get; set; }
    }
}