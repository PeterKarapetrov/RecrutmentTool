using Microsoft.EntityFrameworkCore;
using RecrutmentTool.Data;
using RecrutmentTool.Models.ModelDTOs.HttpDelete;
using RecrutmentTool.Models.ModelDTOs.HttpGet;
using RecrutmentTool.Models.ModelDTOs.HttpPost;
using RecrutmentTool.Models.ModelDTOs.HttpPut;
using RecrutmentTool.Data.Models;
using System;
using System.Linq;

namespace RecrutmentTool.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext dbContext;

        public CandidateService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int CreateCandidate(CandidatePostDTOModel candidateModel)
        {
            var recruiter = this.dbContext.Recruiters.FirstOrDefault(r => r.Email == candidateModel.RecruiterEmail);

            if (recruiter == null)
            {
                recruiter = new Recruiter()
                {
                    LastName = candidateModel.RecruiterLastName,
                    Email = candidateModel.RecruiterEmail,
                    Country = candidateModel.RecruiterCountry
                };
            }

            var candidate = new Candidate()
            {
                FirstName = candidateModel.FirstName,
                LastName = candidateModel.LastName,
                Bio = candidateModel.Bio,
                Email = candidateModel.Email,
                Recruiter = recruiter,
            };

            if (DateTime.TryParse(candidateModel.BirthDate, out DateTime candidateBirthdate))
            {
                candidate.BirthDate = candidateBirthdate;
            }
            

            foreach (var skillName in candidateModel.Skills)
            {
                if (!string.IsNullOrEmpty(skillName))
                {
                    var skillFromDb = this.dbContext.Skills.FirstOrDefault(s => s.Name == skillName);

                    if (skillFromDb != null)
                    {
                        candidate.CandidateSkills.Add(new CandidateSkill { Candidate = candidate, Skill = skillFromDb });
                    }
                    else
                    {
                        var newSkill = new Skill { Name = skillName };

                        candidate.CandidateSkills.Add(new CandidateSkill { Candidate = candidate, Skill = newSkill });
                    }
                }            
            }


            this.dbContext.Candidates.Add(candidate);
            candidate.Recruiter.ExperienceLevel++;
                        
            this.dbContext.SaveChanges();

            // TODO Map to GetDTOModel
            return candidate.Id;
        }

        public CandidateDeleteDTOModel DeleteCandidate(int id)
        {
            var candidateToDelete = this.dbContext.Candidates
                .Include(c => c.Recruiter)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefault(u => u.Id == id);

            if (candidateToDelete != null)
            {
                this.dbContext.Remove(candidateToDelete);
                this.dbContext.SaveChanges();
            }

            return new CandidateDeleteDTOModel()
            {
                FirstName = candidateToDelete.FirstName,
                LastName = candidateToDelete.LastName,
                Email = candidateToDelete.Email,
                Bio = candidateToDelete.Bio,
                BirthDate = candidateToDelete.BirthDate.ToString("yyyy-MM-dd"),
                Recruiter = new RecruiterGetDTOModel()
                {
                    LastName = candidateToDelete.Recruiter.LastName,
                    Email = candidateToDelete.Recruiter.Email,
                    Country = candidateToDelete.Recruiter.Country
                }
            };
        }

        public CandidateGetDTOModel GetCandidateById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var candidate = this.dbContext.Candidates
                .Include(c => c.Recruiter)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefault(u => u.Id == id);

            if (candidate == null)
            {
                return null;
            }

            var candidateModel = new CandidateGetDTOModel()
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                Bio = candidate.Bio,
                BirthDate = candidate.BirthDate.ToString("yyyy-MM-dd"),
                Recruiter = new RecruiterGetDTOModel() {
                    LastName = candidate.Recruiter.LastName,
                    Email = candidate.Recruiter.Email,
                    Country = candidate.Recruiter.Country,
                    Level = candidate.Recruiter.ExperienceLevel.ToString()
                }          
            };

            foreach (var skill in candidate.CandidateSkills)
            {
                candidateModel.Skills.Add(new SkillGetDTOModel()
                {
                    Name = skill.Skill.Name
                });
            }
            
            return candidateModel;
        }

        public bool UpdateCandidateInfo(int id, CandidatePutDTOModel candidateModel)
        {
            var candidateToUpdate = this.dbContext.Candidates
                .Include(c => c.Recruiter)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefault(c => c.Id == id);

            if (candidateToUpdate == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(candidateModel.FirstName) && !string.IsNullOrWhiteSpace(candidateModel.FirstName) )
            {
                candidateToUpdate.FirstName = candidateModel.FirstName;
            }
            
            if (!string.IsNullOrEmpty(candidateModel.LastName) && !string.IsNullOrWhiteSpace(candidateModel.LastName))
            {
                candidateToUpdate.LastName = candidateModel.LastName;
            }

            if (!string.IsNullOrEmpty(candidateModel.Bio) && !string.IsNullOrWhiteSpace(candidateModel.Bio))
            {
                candidateToUpdate.Bio = candidateModel.Bio;
            }

            if (!string.IsNullOrEmpty(candidateModel.BirthDate) && !string.IsNullOrWhiteSpace(candidateModel.BirthDate))
            {
                if (DateTime.TryParse(candidateModel.BirthDate, out DateTime birthdate))
                {
                    candidateToUpdate.BirthDate = birthdate;
                }
            }

            if (!string.IsNullOrEmpty(candidateModel.Email) && !string.IsNullOrWhiteSpace(candidateModel.Email))
            {
                candidateToUpdate.Email = candidateModel.Email;
            }

            //TODO Check if we want to change the recruiter
            if (!string.IsNullOrEmpty(candidateModel.RecruiterLastName) && !string.IsNullOrWhiteSpace(candidateModel.RecruiterLastName))
            {
                candidateToUpdate.Recruiter.LastName = candidateModel.RecruiterLastName;            
            }

            if (!string.IsNullOrEmpty(candidateModel.RecruiterEmail) && !string.IsNullOrWhiteSpace(candidateModel.RecruiterEmail))
            {
                candidateToUpdate.Recruiter.Email = candidateModel.RecruiterEmail;
            }

            if (!string.IsNullOrEmpty(candidateModel.RecruiterCountry) && !string.IsNullOrWhiteSpace(candidateModel.RecruiterCountry))
            {
                candidateToUpdate.Recruiter.Country = candidateModel.RecruiterCountry;
            }

            if (candidateModel.Skills.Count() > 0)
            {

                foreach (var skillName in candidateModel.Skills)
                {

                    if (string.IsNullOrEmpty(skillName) || string.IsNullOrWhiteSpace(skillName))
                    {
                        continue;
                    }

                    if (!candidateToUpdate.CandidateSkills.Select(cs => cs.Skill.Name).Contains(skillName))
                    {
                        var skill = this.dbContext.Skills.FirstOrDefault(s => s.Name == skillName);

                        if (skill == null)
                        {
                            skill = new Skill() { Name = skillName };
                        }

                        candidateToUpdate.CandidateSkills.Add(new CandidateSkill() { Candidate = candidateToUpdate, Skill = skill });
                    }
                }
            }

            this.dbContext.Update(candidateToUpdate);
            var result = this.dbContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
    }
}
