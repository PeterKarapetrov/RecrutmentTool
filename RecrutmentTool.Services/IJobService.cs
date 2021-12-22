using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.ModelDTOs.HttpPost;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Services
{
    public interface IJobService
    {
        int CreateJob(JobPostDTOModel job);

        ICollection<JobGetDTOModel> GetJobsBySkill(string skillName);

        JobGetDTOModel DeleteJob(int jobId);
    }
}
