using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyMiniChallenge.Models
{
    public class Operation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Concept Field is required.")]
        [StringLength(50)]
        public string Concept { get; set; }

        public double Amount { get; set; }

        public TypeEnum Type{ get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser IdentityUser { get; set; }
    }
}
