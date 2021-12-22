using RecrutmentTool.Models.ModelDTOs.HttpDelete;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Models.ModelDTOs.HttpPost;
using RecrutmentTool.Models.ModelDTOs.HttpPut;

namespace RecrutmentTool.Services
{
    public interface ICandidateService
    {
        int CreateCandidate(CandidatePostDTOModel candidate);

        CandidateGetDTOModel GetCandidateById(int id);

        bool UpdateCandidateInfo(int id, CandidatePutDTOModel candidateModel);

        CandidateDeleteDTOModel DeleteCandidate(int id);
    }
}
