using System;
using System.Collections.Generic;
using System.Text.Json;

namespace edu_services.Domain
{
    /// <summary>
    /// The classroom object a generic Teacher & list of generic Students. Helper methods have been added to facilitate adding each & to retrieve the roster.
    /// </summary>
    /// <typeparam name="T">Generic type for a Teacher</typeparam>
    /// <typeparam name="S">Generic type for a Student</typeparam>
    public class Classroom<T, S>
    {
        public Classroom()
        {
            Students = new List<S>();
        }

        public Classroom(List<S> listStudents, T Teacher)
        {
            this.Students = listStudents;
            this.Teacher = Teacher;
        }

        public T Teacher { get; set; }
        public List<S> Students { get; set; }

        /// <summary>
        /// Adds a teacher to the classroom.
        /// </summary>
        /// <param name="teacher">The teacher object</param>
        public void AddTeacher(T teacher)
        {
            Teacher = teacher;
        }

        /// <summary>
        /// Adds a student to the classroom.
        /// </summary>
        /// <param name="student">The student object</param>
        public void AddStudent(S student)
        {
            Students.Add(student);
        }

        /// <summary>
        /// Returns the representations of the teachers & students.
        /// </summary>
        /// <returns></returns>
        public (T, List<S>) GetRoster()
        {
            if (Teacher == null)
                throw new Exception("Classroom contains no teacher.");

            if (Students == null || Students.Count > 3)
                throw new Exception("Classroom does not contain 3 students.");

            return (Teacher, Students);
        }

    }
}
