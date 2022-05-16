using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public class StudentRepository
    {
        private readonly SchoolContext _context;
        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student GetById(Guid studentId)
        {
            //Student student = _context.Students.Include(i => i.Enrollments).Single(w => w.Id == studentId);
            //if (student == null) return null;
            //return student;

            Student student = _context.Students.Find(studentId);           
            if (student == null) return null;

            _context.Entry(student).Collection(x => x.Enrollments).Load();

            return student;
        }
        public void Save(Student student)
        {
            _context.Students.Attach(student);
        }

    }
}
