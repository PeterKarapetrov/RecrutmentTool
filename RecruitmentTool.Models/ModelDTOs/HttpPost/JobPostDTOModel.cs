using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutmentTool.Models.ModelDTOs.HttpPost
{
    public class JobPostDTOModel
    {
        public JobPostDTOModel()
        {
            this.Skills = new List<string>();
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [Range(0, 50000)]
        public decimal Salary { get; set; }

        [Required]
        public ICollection<string> Skills { get; set; }
    }
}
