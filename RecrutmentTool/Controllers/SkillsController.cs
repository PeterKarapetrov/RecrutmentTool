using Microsoft.AspNetCore.Mvc;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Services;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService skillService;

        public SkillsController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        [HttpGet("{id}")]
        public ActionResult<SkillGetDTOModel> Get(int id)
        {
            if (id <= 0 || id > int.MaxValue)
            {
                return BadRequest($"Invalid Id format. It should be a number greater than zero and lower than {int.MaxValue}");
            }

            var skillModel = this.skillService.GetById(id);

            if (skillModel != null)
            {
                return skillModel;
            }
         
            return BadRequest($"No skill with Id: {id}");
        }

        [HttpGet("active")]
        public ActionResult<IEnumerable<SkillGetDTOModel>> Get()
        {
            var skillsModel = this.skillService.GetActiveSkills().ToList();

            if (skillsModel != null)
            {
                return skillsModel;
            }

            return BadRequest($"No skills available");
        }
    }
}
