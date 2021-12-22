using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecrutmentTool.Data;
using RecrutmentTool.Data.Models;
using RecrutmentTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecrutmentTool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDbContext>();

            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<IInterviewService, InterviewService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IRecruiterService, RecruiterService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var db = new ApplicationDbContext())
            {
                db.Database.EnsureCreated();
                if (!db.Recruiters.Any())
                {
                    db.Recruiters.Add(new Recruiter() { LastName = "Petrova", Country = "Bulgaria" , Email = "petrova@softuni.bg" });
                    db.Recruiters.Add(new Recruiter() { LastName = "Ivanova", Country = "USA", Email = "petrova@softuni.us" });
                    db.Recruiters.Add(new Recruiter() { LastName = "Dimitrova", Country = "Germany", Email = "petrova@softuni.de" });
                    db.SaveChanges();

                    db.Skills.Add(new Skill() { Name = "Angular" });
                    db.Skills.Add(new Skill() { Name = "C#" });
                    db.Skills.Add(new Skill() { Name = "VueJs" });

                    db.Candidates.Add(new Candidate() { FirstName = "Peter", LastName = "Karapetrov", Email = "p.karapetrov@hotmail.bg",
                        Bio = "Very good professional", BirthDate = DateTime.Parse("1977-03-06"), RecruiterId = 1 });

                    db.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
