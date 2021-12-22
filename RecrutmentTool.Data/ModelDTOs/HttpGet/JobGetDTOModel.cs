using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutmentTool.Data.ModelDTOs.HttpGet
{
    public class JobGetDTOModel
    {
        public JobGetDTOModel()
        {
            this.Skills = new List<string>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Salary { get; set; }

        public ICollection<string> Skills { get; set; }
    }
}
