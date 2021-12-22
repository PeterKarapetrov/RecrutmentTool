using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutmentTool.Models.ModelDTOs.HttpPut
{
    public class CandidatePutDTOModel
    {

        public CandidatePutDTOModel()
        {
            this.Skills = new List<string>();
        }

        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Bio { get; set; }

        public string BirthDate { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string RecruiterLastName { get; set; }

        [EmailAddress]
        public string RecruiterEmail { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string RecruiterCountry { get; set; }

        public ICollection<string> Skills { get; set; }
    }
}
