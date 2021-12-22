using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecrutmentTool.Models.ModelDTOs.HttpPost
{
    public class CandidatePostDTOModel
    {
        public CandidatePostDTOModel()
        {
            this.Skills = new List<string>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Bio { get; set; }

        public string BirthDate { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string RecruiterLastName { get; set; }

        [Required]
        [EmailAddress]
        public string RecruiterEmail { get; set; }


        public string RecruiterCountry { get; set; }

        [Required]
        public ICollection<string> Skills { get; set; }
    }
}
