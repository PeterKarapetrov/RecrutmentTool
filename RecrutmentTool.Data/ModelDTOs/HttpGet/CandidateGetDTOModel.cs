using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Data.ModelDTOs.HttpGet
{
    public class CandidateGetDTOModel
    {
        public CandidateGetDTOModel()
        {
            this.Skills = new List<SkillGetDTOModel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string BirthDate { get; set; }

        public RecruiterGetDTOModel Recruiter { get; set; }

        public ICollection<SkillGetDTOModel> Skills { get; set; }
    }
}
