using System;
using System.Collections.Generic;

namespace UniversityManagement
{
    abstract class Person
    {
        private int id;
        private string name;

        public int Id => id;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected Person(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public abstract void DisplayInfo();
    }

    class Course
    {
        public int Id { get; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public Faculty Instructor { get; set; }

        private List<Student> enrolledStudents = new List<Student>();
        public List<Student> EnrolledStudents => enrolledStudents;

        public Course(int id, string title, int credits)
        {
            Id = id;
            Title = title;
            Credits = credits;
        }

        public void EnrollStudent(Student student)
        {
            if (!enrolledStudents.Contains(student))
            {
                enrolledStudents.Add(student);
            }
        }

        public void DisplayInfo()
        {
            string instructorName = Instructor != null ? Instructor.Name : "TBA";
            Console.WriteLine($"Course: {Title} | Credits: {Credits} | Instructor: {instructorName} | Enrolled: {enrolledStudents.Count}");
        }
    }

    class Student : Person
    {
        public string Major { get; set; }

        private List<Course> enrolledCourses = new List<Course>();
        private Dictionary<Course, string> grades = new Dictionary<Course, string>();

        public Student(int id, string name, string major) : base(id, name)
        {
            Major = major;
        }

        public void EnrollInCourse(Course course)
        {
            if (!enrolledCourses.Contains(course))
            {
                enrolledCourses.Add(course);
                course.EnrollStudent(this);
                Console.WriteLine($"{Name} enrolled in {course.Title}");
            }
        }

        public void AssignGrade(Course course, string grade)
        {
            if (enrolledCourses.Contains(course))
            {
                grades[course] = grade;
            }
        }

        public void ShowTranscript()
        {
            Console.WriteLine($"\n{Name}'s Transcript:");

            if (enrolledCourses.Count == 0)
            {
                Console.WriteLine("No courses enrolled.");
                return;
            }

            foreach (Course course in enrolledCourses)
            {
                string grade = grades.ContainsKey(course) ? grades[course] : "In Progress";
                Console.WriteLine($"{course.Title} - Grade: {grade}");
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Student: {Name} | ID: {Id} | Major: {Major} | Courses: {enrolledCourses.Count}");
        }
    }

    class Faculty : Person
    {
        public string Department { get; set; }
        private List<Course> coursesTaught = new List<Course>();

        public Faculty(int id, string name, string department) : base(id, name)
        {
            Department = department;
        }

        public void AssignCourse(Course course)
        {
            coursesTaught.Add(course);
            course.Instructor = this;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Faculty: {Name} | ID: {Id} | Department: {Department} | Courses Taught: {coursesTaught.Count}");
        }
    }

    class University
    {
        private List<Student> students = new List<Student>();
        private List<Faculty> facultyMembers = new List<Faculty>();
        private List<Course> courses = new List<Course>();

        public void AddStudent(Student student) => students.Add(student);
        public void AddFaculty(Faculty faculty) => facultyMembers.Add(faculty);
        public void AddCourse(Course course) => courses.Add(course);

        public Student FindStudent(int id)
        {
            foreach (Student s in students)
            {
                if (s.Id == id)
                    return s;
            }
            return null;
        }

        public Course FindCourse(int id)
        {
            foreach (Course c in courses)
            {
                if (c.Id == id)
                    return c;
            }
            return null;
        }

        public void DisplayAllStudents()
        {
            Console.WriteLine("\nAll Students:");
            foreach (var s in students) s.DisplayInfo();
        }

        public void DisplayAllFaculty()
        {
            Console.WriteLine("\nAll Faculty:");
            foreach (var f in facultyMembers) f.DisplayInfo();
        }

        public void DisplayAllCourses()
        {
            Console.WriteLine("\nAll Courses:");
            foreach (var c in courses) c.DisplayInfo();
        }
    }

    class Admin
    {
        public string Name { get; set; }
        private University university;

        public Admin(string name, University university)
        {
            Name = name;
            this.university = university;
        }

        public void RegisterStudent(Student student)
        {
            university.AddStudent(student);
            Console.WriteLine($"Admin {Name} registered student {student.Name}");
        }

        public void RegisterFaculty(Faculty faculty)
        {
            university.AddFaculty(faculty);
            Console.WriteLine($"Admin {Name} registered faculty {faculty.Name}");
        }

        public void CreateCourse(Course course)
        {
            university.AddCourse(course);
            Console.WriteLine($"Admin {Name} created course {course.Title}");
        }

        public void AssignGrade(Student student, Course course, string grade)
        {
            student.AssignGrade(course, grade);
            Console.WriteLine($"Admin {Name} assigned grade {grade} to {student.Name} for {course.Title}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            University uni = new University();
            Admin admin = new Admin("Mr. Karim", uni);

            Student s1 = new Student(1, "Yeamin", "CSE");
            Faculty f1 = new Faculty(101, "Dr. Ashraful", "CSE");
            Course c1 = new Course(501, "Data Structures", 3);

            admin.RegisterStudent(s1);
            admin.RegisterFaculty(f1);
            admin.CreateCourse(c1);

            f1.AssignCourse(c1);
            s1.EnrollInCourse(c1);

            admin.AssignGrade(s1, c1, "A");

            uni.DisplayAllStudents();
            uni.DisplayAllFaculty();
            uni.DisplayAllCourses();

            s1.ShowTranscript();

            Console.ReadLine();
        }
    }
}