using Microsoft.AspNetCore.Mvc;
using RecrutmentTool.Models.ModelDTOs.HttpDelete;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Models.ModelDTOs.HttpPost;
using RecrutmentTool.Models.ModelDTOs.HttpPut;
using RecrutmentTool.Services;


namespace RecrutmentTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        [HttpPost]
        public ActionResult Post(CandidatePostDTOModel candidateModel)
        {
            var newCandidateId = this.candidateService.CreateCandidate(candidateModel);

            if (newCandidateId != 0)
            {
                return CreatedAtAction("Get", new { id = newCandidateId }, candidateModel);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public ActionResult<CandidateGetDTOModel> Get(int id)
        {
            if (id <= 0 || id > int.MaxValue)
            {
                return BadRequest($"Invalid Id format. It should be a number greater than zero and lower than {int.MaxValue}");
            }

            var candidateModel = candidateService.GetCandidateById(id);
            
            if (candidateModel != null)
            {
                return candidateModel;
            }

            return BadRequest($"No user with Id: {id}");
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, CandidatePutDTOModel candidateModel)
        {
            if (id <= 0 || id > int.MaxValue)
            {
                return BadRequest($"Invalid Id format. It should be a number greater than zero and lower than {int.MaxValue}");
            }

            if (candidateModel == null)
            {
                return BadRequest("Invalid update format!");
            }

            var updateSucceed = this.candidateService.UpdateCandidateInfo(id, candidateModel);

            if (updateSucceed)
            {
                return Ok("Candidate Info Update Sucssed!");
            }

            return BadRequest("Candidate Info Update Failed!");

        }

        [HttpDelete("{id}")]
        public ActionResult<CandidateDeleteDTOModel> Delete(int id)
        {
            if (id <= 0 || id > int.MaxValue)
            {
                return BadRequest($"Invalid Id format. It should be a number greater than zero and lower than {int.MaxValue}");
            }

            var candidate = this.candidateService.DeleteCandidate(id);

            return candidate;
        }

    }
}
