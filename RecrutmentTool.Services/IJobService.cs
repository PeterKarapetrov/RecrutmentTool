using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Models.ModelDTOs.HttpPost;
using System.Collections.Generic;

namespace RecrutmentTool.Services
{
    public interface IJobService
    {
        int CreateJob(JobPostDTOModel job);

        ICollection<JobGetDTOModel> GetJobsBySkill(string skillName);

        JobGetDTOModel DeleteJob(int jobId);
    }
}
