using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RecrutmentTool.Data.Models
{
    public class Recruiter
    {

        public Recruiter()
        {
            this.InterviewSlots = new List<Interview>(5);
            this.Candidates = new List<Candidate>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string  Email { get; set; }

        [Required]
        public string Country { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<Interview> InterviewSlots { get; set; }

        public int ExperienceLevel { get; set; } = 1;
    }
}
