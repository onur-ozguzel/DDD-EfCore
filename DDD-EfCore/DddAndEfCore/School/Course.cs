using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    // Enumeration Pattern
    public class Course : Entity
    {
        public static readonly Course Calculus = new Course(Guid.Parse("1b302e6f-81d6-4add-b8ef-424a4f685dd1"), "Calculus");
        public static readonly Course Chemistry = new Course(Guid.Parse("5d77a202-5c7a-44a3-99f9-f3fe3d34a0d3"), "Chemistry");
        public static readonly Course[] AllCourses = { Calculus, Chemistry };
        public string Name { get; }

        protected Course()
        {

        }

        public Course(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public static Course FromId(Guid id)
        {
            return AllCourses.SingleOrDefault(w => w.Id == id);
        }
    }
}
