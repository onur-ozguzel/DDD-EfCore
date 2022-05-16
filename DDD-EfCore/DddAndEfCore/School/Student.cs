using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public class Student : AggregateRoot
    {
        public Student(Name name, Email email, Course favoriteCourse, Grade favoriteGrade) : this()
        {
            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;

            this.EnrollIn(favoriteCourse, favoriteGrade);
        }

        protected Student()
        {

        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public virtual Course FavoriteCourse { get; private set; }

        private readonly List<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();

        public string EnrollIn(Course course, Grade grade)
        {
            if (_enrollments.Any(w => w.Course == course)) return $"Already enrolled in course '{course.Name}'";

            var enrollment = new Enrollment(course, this, grade);
            _enrollments.Add(enrollment);

            return "OK";
        }

        public void Disenroll(Course course)
        {
            Enrollment enrollment = _enrollments.FirstOrDefault(w => w.Course == course);
            if (enrollment == null) return;

            _enrollments.Remove(enrollment);
        }

        public void EditPersonalInfo(Name name, Email email, Course favoriteCourse)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (favoriteCourse == null) throw new ArgumentNullException(nameof(favoriteCourse));

            if (Email != email) RaiseDomainEvent(new StudentEmailChangedEvent(Id, email));

            Name = name;
            Email = email;
            FavoriteCourse = favoriteCourse;
        }
    }
}
