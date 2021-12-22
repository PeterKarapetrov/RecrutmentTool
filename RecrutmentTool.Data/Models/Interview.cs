using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecrutmentTool.Data.Models
{
    public class Interview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }

        public Job Job { get; set; }

        [Required]
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}
