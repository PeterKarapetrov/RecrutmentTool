using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Data.Models
{
    public class JobSkill
    {
        public int JobId { get; set; }

        public Job Job { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}
