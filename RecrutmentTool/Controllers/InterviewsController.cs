
using Microsoft.AspNetCore.Mvc;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Services;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewService interviewService;

        public InterviewsController(IInterviewService interviewService)
        {
            this.interviewService = interviewService;
        }

        [HttpGet]
        [ActionName("Interviews")]
        public ActionResult<IEnumerable<InterviewGetDTOModel>> Get()
        {
            return this.interviewService.GetAll().ToArray();
        }
    }
}
