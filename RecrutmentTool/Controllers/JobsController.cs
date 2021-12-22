using Microsoft.AspNetCore.Mvc;
using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.ModelDTOs.HttpPost;
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
    public class JobsController : ControllerBase
    {
        private readonly IJobService jobService;

        public JobsController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost]
        public ActionResult<JobPostDTOModel> Post(JobPostDTOModel job)
        {
            var newJobId = this.jobService.CreateJob(job);

            if (newJobId != 0)
            {
                return CreatedAtAction("Get", new { skill = job.Skills.First() }, job);
            }

            return BadRequest("Job create failed!");
        }

        [HttpGet("{skill}")]
        public ActionResult<IEnumerable<JobGetDTOModel>> Get(string skillName)
        {
            return this.jobService.GetJobsBySkill(skillName).ToArray();
        }

        [HttpDelete("{id}")]
        public ActionResult<JobGetDTOModel> Delete(int id)
        {
            if (id <= 0 || id > int.MaxValue)
            {
                return BadRequest($"Invalid Id format. It should be a number greater than zero and lower than {int.MaxValue}");
            }

            var jobToDelete = this.jobService.DeleteJob(id);

            if (jobToDelete != null)
            {
                return jobToDelete;
            }

            return BadRequest($"No Job with Id: {id}");
        }
    }
}
