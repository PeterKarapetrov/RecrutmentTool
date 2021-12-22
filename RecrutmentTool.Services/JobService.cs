using Microsoft.EntityFrameworkCore;
using RecrutmentTool.Data;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Models.ModelDTOs.HttpPost;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IInterviewService interviewService;

        public JobService(ApplicationDbContext dbContext, IInterviewService interviewService)
        {
            this.dbContext = dbContext;
            this.interviewService = interviewService;
        }

        public int CreateJob(JobPostDTOModel jobModel)
        {
            var newJob = new Job()
            {
                Title = jobModel.Title,
                Description = jobModel.Description,
                Salary = jobModel.Salary
            };

            foreach (var skillName in jobModel.Skills)
            {
                if (!string.IsNullOrEmpty(skillName))
                {
                    var skillFromDb = this.dbContext.Skills.FirstOrDefault(s => s.Name == skillName);

                    if (skillFromDb != null)
                    {
                        newJob.RequiredSkills.Add(new JobSkill { Job = newJob, Skill = skillFromDb });
                    }
                    else
                    {
                        var newSkill = new Skill { Name = skillName };

                        newJob.RequiredSkills.Add(new JobSkill { Job = newJob, Skill = newSkill });
                    }
                }
            }

            this.dbContext.Jobs.Add(newJob);
            this.dbContext.SaveChanges();

            this.interviewService.CreateInterviewsForJob(newJob);

            return newJob.Id;
        }

        public JobGetDTOModel DeleteJob(int jobId)
        {
            var jobToDelete = this.dbContext.Jobs.FirstOrDefault(j => j.Id == jobId);

            if (jobToDelete == null)
            {
                return null;
            }

            this.dbContext.Jobs.Remove(jobToDelete);
            this.dbContext.SaveChanges();
            
            return new JobGetDTOModel() 
            {
                Title = jobToDelete.Title,
                Description = jobToDelete.Description,
                Salary = jobToDelete.Salary.ToString()        
            };
        }

        public ICollection<JobGetDTOModel> GetJobsBySkill(string skillName)
        {
            var skill = this.dbContext.Skills.FirstOrDefault(s => s.Name == skillName);

            if (skill == null)
            {
                return null;
            }

            var jobsWithSkill = this.dbContext.Jobs
                                              .Include(j => j.RequiredSkills)
                                              .ThenInclude(js => js.Skill)
                                              .Where(j => j.RequiredSkills.Select(rs => rs.Skill)
                                              .Contains(skill));

            var jobsModelList = new List<JobGetDTOModel>();

            foreach (var job in jobsWithSkill)
            {
                jobsModelList.Add(new JobGetDTOModel()
                {
                    Title = job.Title,
                    Description = job.Description,
                    Salary = job.Salary.ToString()
                });
            }

            return jobsModelList;
        }
    }
}
