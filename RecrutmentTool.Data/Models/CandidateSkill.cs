using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Data.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}
