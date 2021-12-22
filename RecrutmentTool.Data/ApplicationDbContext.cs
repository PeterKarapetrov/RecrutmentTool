using Microsoft.EntityFrameworkCore;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Recruiter> Recruiters { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobSkill> JobSkills { get; set; }

        public DbSet<CandidateSkill> CandidateSkills { get; set; }

        public DbSet<Interview> Interviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSettings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateSkill>().HasKey(cs => new { cs.CandidateId, cs.SkillId });

            modelBuilder.Entity<JobSkill>().HasKey(cs => new { cs.JobId, cs.SkillId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
