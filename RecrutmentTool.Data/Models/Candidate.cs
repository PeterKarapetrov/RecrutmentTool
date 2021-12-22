using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecrutmentTool.Data.Models
{
    public class Candidate
    {
        public Candidate()
        {
            this.CandidateSkills = new List<CandidateSkill>();
            this.Interviews = new List<Interview>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Bio { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public int RecruiterId { get; set; }

        public Recruiter Recruiter { get; set; }

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
