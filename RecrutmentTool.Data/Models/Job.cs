using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecrutmentTool.Data.Models
{
    public class Job
    {

        public Job()
        {
            this.RequiredSkills = new List<JobSkill>();
            this.Interviews = new List<Interview>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Salary { get; set; }

        [Required]
        public virtual ICollection<JobSkill> RequiredSkills { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
