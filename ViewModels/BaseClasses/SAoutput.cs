using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses
{
    public class SAoutput
    {
        public string TotalCourseNumber
        {
            get; set;
        }

        public string TotalCreditHours { get; set; }

        public string WeightedMark { get; set; }

        public string SemesterAverage { get; set; }
    }
}
