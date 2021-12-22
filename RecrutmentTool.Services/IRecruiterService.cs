using RecrutmentTool.Data.ModelDTOs.HttpGet;
using RecrutmentTool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Services
{
    public interface IRecruiterService
    {
        IEnumerable<RecruiterGetDTOModel> GetAll();

        IEnumerable<RecruiterGetDTOModel> GetAllByLevel(int level);

        bool RecruiterExists(string recruiterEmail);
    }
}
