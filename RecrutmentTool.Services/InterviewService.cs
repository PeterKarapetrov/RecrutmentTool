using Microsoft.EntityFrameworkCore;
using RecrutmentTool.Data;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly ApplicationDbContext dbContext;

        public InterviewService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<InterviewGetDTOModel> GetAll()
        {
            var interviewsFromDb = this.dbContext.Interviews
                                                .Include(i => i.Job)
                                                .Include(i => i.Candidate)
                                                .ThenInclude(c => c.Recruiter);

            var interviewsModelList = new List<InterviewGetDTOModel>();

            foreach (var interview in interviewsFromDb)
            {
                interviewsModelList.Add(new InterviewGetDTOModel()
                {
                    CandidateFullName = interview.Candidate.FirstName + " " + interview.Candidate.LastName,
                    CandidateEmail = interview.Candidate.Email,
                    RecruiterFullName = interview.Candidate.Recruiter.LastName,
                    RecruiterEmail = interview.Candidate.Recruiter.Email,
                    JobTitle = interview.Job.Title,
                    JobDescription = interview.Job.Description
                });
            }

            return interviewsModelList;
        }

        public void CreateInterviewsForJob(Job job)
        {
            var jobSkills = job.RequiredSkills.Select(rs => rs.Skill);

            var applicableCandidates = this.dbContext.Candidates
                                                   .Include(c => c.Recruiter)
                                                   .Include(c => c.CandidateSkills)
                                                   .ThenInclude(cs => cs.Skill)
                                                   .Where(c => c.CandidateSkills.Select(cs => cs.Skill).Any(s => jobSkills.Contains(s)));

            var jobInterviewsList = new List<Interview>();

            foreach (var candidate in applicableCandidates)
            {
                if (candidate.Recruiter.InterviewSlots.Count() < 5)
                {
                    var newInterview = new Interview() { Candidate = candidate, Job = job };

                    jobInterviewsList.Add(newInterview);
                    candidate.Recruiter.InterviewSlots.Add(newInterview);
                    candidate.Recruiter.ExperienceLevel++;
                }   
            }

            this.dbContext.Interviews.AddRange(jobInterviewsList);
            this.dbContext.SaveChanges();
        }
    }
}
