using Microsoft.AspNetCore.Mvc;
using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using RecrutmentTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecrutmentTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecruitersController : ControllerBase
    {
        private readonly IRecruiterService recruiterService;

        public RecruitersController(IRecruiterService recruiterService)
        {
            this.recruiterService = recruiterService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RecruiterGetDTOModel>> Get()
        {
            return this.recruiterService.GetAll().ToList();
        }

        [HttpGet("{level}")]
        public ActionResult<IEnumerable<RecruiterGetDTOModel>> Get(int level)
        {
            return this.recruiterService.GetAllByLevel(level).ToList();
        }
    }
}
