using DddAndEfCore.School;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore
{
    /// <summary>
    /// Example purposes
    /// </summary>
    public sealed class StudentController
    {
        private readonly SchoolContext _context;
        private readonly StudentRepository _repository;
        public StudentController(SchoolContext context)
        {
            _context = context;
            _repository = new StudentRepository(context);
        }

        public string CheckStudentFavoriteCourse(Guid studentId, Guid courseId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null) return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null) return "Course not found";

            return student.FavoriteCourse == course ? "Yes" : "No";
        }

        public string EnrollStudent(Guid studentId, Guid courseId, Grade grade)
        {
            Student student = _repository.GetById(studentId);
            if (student == null) return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null) return "Course not found";

            var result = student.EnrollIn(course, grade);

            _context.SaveChanges();

            return result;
        }

        public string DisenrollStudent(Guid studentId, Guid courseId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null) return "Student not found";

            Course course = Course.FromId(courseId);
            if (course == null) return "Course not found";

            student.Disenroll(course);

            _context.SaveChanges();

            return "OK";
        }

        public string RegisterStudent(string firstName, string lastName, string email, Guid favoriteCourseId, Grade favoriteCourseGrade, Guid nameSuffixId)
        {
            Course favoriteCourse = Course.FromId(favoriteCourseId);
            if (favoriteCourse == null) return "Course not found";

            Suffix suffix = Suffix.FromId(nameSuffixId);
            if (suffix == null) return "Course not found";

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure) return emailResult.Error;

            var nameResult = Name.Create(firstName, lastName, suffix);
            if (nameResult.IsFailure) return nameResult.Error;

            var student = new Student(nameResult.Value, emailResult.Value, favoriteCourse, favoriteCourseGrade);
            _repository.Save(student);
            _context.SaveChanges();

            return "OK";
        }

        public string EditPersonalInformation(Guid studentId, string firstName, string lastName, string email, Guid favoriteCourseId, Guid nameSuffixId)
        {
            Student student = _repository.GetById(studentId);
            if (student == null) return "Student not found";

            Suffix suffix = Suffix.FromId(nameSuffixId);
            if (suffix == null) return "Course not found";

            Course favoriteCourse = Course.FromId(favoriteCourseId);
            if (favoriteCourse == null) return "Course not found";

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure) return emailResult.Error;

            var nameResult = Name.Create(firstName, lastName, suffix);
            if (nameResult.IsFailure) return nameResult.Error;

            student.EditPersonalInfo(nameResult.Value, emailResult.Value, favoriteCourse);

            _context.SaveChanges();

            return "OK";
        }
    }
}
