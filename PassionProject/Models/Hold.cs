using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class Hold
    {
        [Key]
        public int HoldID { get; set; }
        //a Problem can have many holds but a hold can only have 1 problem
        [ForeignKey("Problem")]
        public int ProblemID { get; set; }
        public virtual Problem Problem { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    public class HoldDto
    {
        public int HoldID { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}