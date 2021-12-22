using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Services
{
    public interface IInterviewService
    {
        ICollection<InterviewGetDTOModel> GetAll();

        void CreateInterviewsForJob(Job job);
    }
}
