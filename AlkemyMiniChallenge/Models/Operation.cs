using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyMiniChallenge.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Concept { get; set; }
        public double Amount { get; set; }
        public TypeEnum Type{ get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
