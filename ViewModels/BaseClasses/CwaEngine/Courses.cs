using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.CwaEngine
{
    public class Courses
    {
        public Courses() { }

        public Courses(Courses course)
        {
            CourseCode = course.CourseCode;
            CourseName = course.CourseName;
            CreditHour = course.CreditHour;
            Mark = course.Mark;
        }
        public string CourseCode
        {
            get; set;
        }

        public string CourseName { get; set; }

        public string CreditHour { get; set; }

        public string Mark { get; set; }

    }
}
