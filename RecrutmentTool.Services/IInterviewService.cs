using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using System.Collections.Generic;

namespace RecrutmentTool.Services
{
    public interface IInterviewService
    {
        ICollection<InterviewGetDTOModel> GetAll();

        void CreateInterviewsForJob(Job job);
    }
}
