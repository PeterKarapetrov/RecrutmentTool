using RecrutmentTool.Models.ModelDTOs.HttpGet;
using System.Collections.Generic;

namespace RecrutmentTool.Services
{
    public interface IRecruiterService
    {
        IEnumerable<RecruiterGetDTOModel> GetAll();

        IEnumerable<RecruiterGetDTOModel> GetAllByLevel(int level);

        bool RecruiterExists(string recruiterEmail);
    }
}
