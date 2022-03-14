using Npgsql;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.CRUD.Repositories
{
    public class TeacherRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=root;";
        readonly NpgsqlConnection con = new(connectionString);
        public List<Teacher> GetTeachers()
        {
            con.Open();
            List<Teacher> teachers = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select teacher.id,teacher.salary,teacher.name,teacher.email,course.id,course.name,course.price from teacher left join course on teacher.id=course.teacher_id";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var teacherId = Convert.ToInt32(reader.GetValue(0));
                var teacherSalary = Convert.ToInt32(reader.GetValue(1));
                var teacherName = reader.GetValue(2).ToString();
                var teacherEmail = reader.GetValue(3).ToString();
                var teacher = teachers.Where(p => p.Id == teacherId).FirstOrDefault();
                if (reader.GetValue(4) == DBNull.Value)
                {
                    teacher = new();
                    teacher.Id = teacherId;
                    teacher.Name = teacherName;
                    teacher.Salary = teacherSalary;
                    teacher.Email = teacherEmail; ;
                    teacher.Courses = new();
                    teachers.Add(teacher);
                }
                else
                {


                    var courseId = Convert.ToInt32(reader.GetValue(4));
                    var courseName = reader.GetValue(5).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(6));

                    if (teacher == null)
                    {
                        teacher = new();
                        teacher.Id = teacherId;
                        teacher.Name = teacherName;
                        teacher.Salary = teacherSalary;
                        teacher.Email = teacherEmail;
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        teacher.Courses = new();
                        teacher.Courses.Add(course);
                        teachers.Add(teacher);
                    }
                    else
                    {
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        if (teacher.Courses != null) if (!teacher.Courses.Contains(course))
                                teacher.Courses.Add(course);
                    }
                }

            }
            reader.Close();
            con.Close();
            return teachers;
        }
        public Teacher GetTeacherById(int id)
        {
            con.Open();
            Teacher teacher = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select teacher.id,teacher.salary,teacher.name,teacher.email,course.id,course.name,course.price from teacher left join course on teacher.id=course.teacher_id where teacher.id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            teacher.Courses = new();
            while (reader.Read())
            {
                teacher.Id = Convert.ToInt32(reader.GetValue(0));
                teacher.Salary = Convert.ToInt32(reader.GetValue(1));
                teacher.Name = reader.GetValue(2).ToString();
                teacher.Email = reader.GetValue(3).ToString();
                if (reader.GetValue(4) == DBNull.Value)
                {
                    teacher.Courses = new();
                }
                else
                {
                    var courseId = Convert.ToInt32(reader.GetValue(4));
                    var courseName = reader.GetValue(5).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(6));
                    Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                    teacher.Courses.Add(course);
                }

            }
            reader.Close();
            con.Close();
            return teacher;
        }

        public void DeleteTeacher(int id)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Delete from teacher where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddTeacher(Teacher teacher)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into teacher(name,salary,email) VALUES(@name,@salary,@email)";
            if (teacher.Name != null) cmd.Parameters.AddWithValue("name", teacher.Name);
            cmd.Parameters.AddWithValue("salary", teacher.Salary);
            if (teacher.Email != null) cmd.Parameters.AddWithValue("email", teacher.Email);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateTeacher(Teacher teacher)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE teacher SET name = @name,salary=@salary,email=@email WHERE id = @id";
            if (teacher.Name != null) cmd.Parameters.AddWithValue("name", teacher.Name);
            cmd.Parameters.AddWithValue("id", teacher.Id);
            cmd.Parameters.AddWithValue("salary", teacher.Salary);
            if (teacher.Email != null) cmd.Parameters.AddWithValue("email", teacher.Email);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


