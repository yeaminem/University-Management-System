using System;
using System.Collections.Generic;

namespace UniversityManagement
{
    abstract class Person
    {
        public int Id { get; }
        public string Name { get; set; }

        protected Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void DisplayInfo();
    }

    class Course
    {
        public int Id { get; }
        public string Title { get; set; }
        public int Credits { get; set; }
        private List<Student> enrolledstudents = new List<Student>();
        public Course(int id, string title, int credits)
        {
            Id = id;
            Title = title;
            Credits = credits;

        }

        public void AddStudent(Student student)
        {
            if (!enrolledstudents.Contains(student))
            {
                enrolledstudents.Add(student);
            }
        }
        public int StudentCount
        {
            get { return enrolledstudents.Count;}
        }
        public void DisplayInfo()
        {

        }
    }
    class Student:Person
    {
        
    }
    class Faculty : Person
    {

    }
    class University
    {

    }
    class Admin
    {

    }
}