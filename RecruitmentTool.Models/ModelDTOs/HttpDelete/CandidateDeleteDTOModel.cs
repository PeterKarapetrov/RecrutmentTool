using RecrutmentTool.Models.ModelDTOs.HttpGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutmentTool.Models.ModelDTOs.HttpDelete
{
    public class CandidateDeleteDTOModel
    {
        public CandidateDeleteDTOModel()
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
