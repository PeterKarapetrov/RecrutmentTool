using RecrutmentTool.Data;
using RecrutmentTool.Data.ModelDTOs.HttpGet;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Services
{
    public class SkillService : ISkillService
    {
        private readonly ApplicationDbContext dbContext;

        public SkillService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SkillGetDTOModel> GetActiveSkills()
        {
            var activeSkillsFromDB = this.dbContext.CandidateSkills.Select(cs => cs.Skill).ToHashSet();

            var skillsModelList = new List<SkillGetDTOModel>();

            foreach (var skill in activeSkillsFromDB)
            {
                skillsModelList.Add(new SkillGetDTOModel() { Name = skill.Name });
            }

            return skillsModelList;
        }

        public SkillGetDTOModel GetById(int id)
        {
            if (id == 0) 
            {
                return null;
            }

            var skill = this.dbContext.Skills.FirstOrDefault(s => s.Id == id);

            if (skill != null)
            {
                return new SkillGetDTOModel() { Name = skill.Name };
            }

            return null;
        }
    }
}
