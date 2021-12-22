using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutmentTool.Models.ModelDTOs.HttpGet
{
    public class RecruiterGetDTOModel
    {
        public RecruiterGetDTOModel()
        {
            this.Candidates = new List<string>();
        }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Level { get; set; }

        public ICollection<string> Candidates { get; set; }
    }
}
