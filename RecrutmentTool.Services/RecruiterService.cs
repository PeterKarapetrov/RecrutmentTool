using Microsoft.EntityFrameworkCore;
using RecrutmentTool.Data;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentTool.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly ApplicationDbContext dbContext;

        public RecruiterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<RecruiterGetDTOModel> GetAll()
        {
            var recruitersFromDb = this.dbContext.Recruiters
                .Include(r => r.Candidates)
                .Where(r => r.Candidates.Count() > 0);

            var recruitersModelList = new List<RecruiterGetDTOModel>();

            foreach (var recruiter in recruitersFromDb)
            {
                var recruiterModel = new RecruiterGetDTOModel()
                {
                    LastName = recruiter.LastName,
                    Email = recruiter.Email,
                    Country = recruiter.Country,
                    Level = recruiter.ExperienceLevel.ToString()
                };

                foreach (var candidate in recruiter.Candidates)
                {
                    var candidateModel = $"FullName: {candidate.FirstName} {candidate.LastName}, Email: {candidate.Email}";
                    recruiterModel.Candidates.Add(candidateModel);
                }

                recruitersModelList.Add(recruiterModel);
            }

            return recruitersModelList;
        }

        public IEnumerable<RecruiterGetDTOModel> GetAllByLevel(int level)
        {
            var recruitersFromDb = this.dbContext.Recruiters
                .Include(r => r.Candidates)
                .Where(r => r.ExperienceLevel == level);

            var recruitersModelList = new List<RecruiterGetDTOModel>();

            foreach (var recruiter in recruitersFromDb)
            {
                var recruiterModel = new RecruiterGetDTOModel()
                {
                    LastName = recruiter.LastName,
                    Email = recruiter.Email,
                    Country = recruiter.Country,
                    Level = recruiter.ExperienceLevel.ToString()
                };

                foreach (var candidate in recruiter.Candidates)
                {
                    var candidateModel = $"FullName: {candidate.FirstName} {candidate.LastName}, Email: {candidate.Email}";
                    recruiterModel.Candidates.Add(candidateModel);
                }

                recruitersModelList.Add(recruiterModel);
            }

            return recruitersModelList;
        }

        public bool RecruiterExists(string recruiterEmail)
        {
            return this.dbContext.Recruiters.Select(r => r.Email).Contains(recruiterEmail);
        }
    }
}
