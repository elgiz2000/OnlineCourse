using Npgsql;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.CRUD.Repositories
{
    public class StudentRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=root;";
        readonly NpgsqlConnection con = new(connectionString);
        public List<Student> GetStudents()
        {
            con.Open();
            List<Student> students = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select student.id,student.name,course.id,course.name,course.price from student left join course_student on student.id=course_student.student_id left join course on course_student.course_id=course.id";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var studentId = Convert.ToInt32(reader.GetValue(0));
                var studentName = reader.GetValue(1).ToString();
                var student = students.Where(p => p.Id == studentId).FirstOrDefault();
                if (reader.GetValue(2) == DBNull.Value)
                {
                    student = new();
                    student.Id = studentId;
                    student.Name = studentName;
                    student.Courses = new();
                    students.Add(student);
                }
                else
                {
                    var courseId = Convert.ToInt32(reader.GetValue(2));
                    var courseName = reader.GetValue(3).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(4));
                    if (student == null)
                    {
                        student = new();
                        student.Id = studentId;
                        student.Name = studentName;
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        student.Courses = new();
                        student.Courses.Add(course);
                        students.Add(student);
                    }
                    else
                    {
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        if (student.Courses != null)
                            if (!student.Courses.Contains(course))
                                student.Courses.Add(course);
                    }
                }

            }
            reader.Close();
            con.Close();
            return students;
        }
        public Student GetStudentById(int id)
        {
            con.Open();
            Student student = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select student.id,student.name,course.id,course.name,course.price from student left join course_student on student.id=course_student.student_id left join course on course_student.course_id=course.id where student.id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            student.Courses = new();
            while (reader.Read())
            {
                var studentId = Convert.ToInt32(reader.GetValue(0));
                var studentName = reader.GetValue(1).ToString();
                if (reader.GetValue(2) == DBNull.Value)
                {
                    student.Id = studentId;
                    student.Name = studentName;
                    student.Courses = new();
                }
                else
                {
                    var courseId = Convert.ToInt32(reader.GetValue(2));
                    var courseName = reader.GetValue(3).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(4));
                    student.Id = studentId;
                    student.Name = studentName;
                    Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                    student.Courses.Add(course);
                }


            }
            reader.Close();
            con.Close();
            return student;
        }

        public void DeleteStudent(int id)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Delete from student where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddStudent(Student student)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into student(name) VALUES(@name)";
            if (student.Name != null)
                cmd.Parameters.AddWithValue("name", student.Name);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateStudent(Student student)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE student SET name = @name WHERE id = @id";
            if (student.Name != null) cmd.Parameters.AddWithValue("name", student.Name);
            cmd.Parameters.AddWithValue("id", student.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


