using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Services
{
    public interface ISkillService
    {
        SkillGetDTOModel GetById(int id);

        IEnumerable<SkillGetDTOModel> GetActiveSkills();
    }
}
