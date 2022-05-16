using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public class Enrollment : Entity
    {
        public Grade Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

        protected Enrollment()
        {

        }

        public Enrollment(Course course, Student student, Grade grade) : this()
        {
            Course = course;
            Student = student;
            Grade = grade;
        }
    }

    public enum Grade
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        F = 4,
    }
}
