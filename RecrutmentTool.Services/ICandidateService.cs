using RecrutmentTool.Data.ModelDTOs.HttpDelete;
using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.ModelDTOs.HttpPost;
using RecrutmentTool.Data.ModelDTOs.HttpPut;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
