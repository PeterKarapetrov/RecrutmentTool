using RecrutmentTool.Models.ModelDTOs.HttpGet;
using System.Collections.Generic;

namespace RecrutmentTool.Services
{
    public interface ISkillService
    {
        SkillGetDTOModel GetById(int id);

        IEnumerable<SkillGetDTOModel> GetActiveSkills();
    }
}
