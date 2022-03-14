using Npgsql;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.CRUD.Repositories
{
    public class CourseStudentRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=root;";
        readonly NpgsqlConnection con = new(connectionString);
        public List<CourseStudentGetDto> GetStudents(int id)
        {
            con.Open();
            List<CourseStudentGetDto> students = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from course_student inner join student on student.id=course_student.student_id where course_student.course_id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CourseStudentGetDto std = new();
                std.Id = Convert.ToInt32(reader.GetValue(1));
                std.Name = reader.GetValue(3).ToString();
                students.Add(std);
            }
            reader.Close();
            con.Close();
            return students;
        }
        public void DeleteStudent(CourseStudentDto courseStudentDto)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Delete from course_student where student_id=@student_id and course_id=@course_id";
            cmd.Parameters.AddWithValue("student_id", courseStudentDto.StudentId);
            cmd.Parameters.AddWithValue("course_id", courseStudentDto.CourseId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AddStudent(CourseStudentDto courseStudentDto)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into course_student(student_id,course_id) VALUES(@student_id,@course_id)";
            cmd.Parameters.AddWithValue("student_id", courseStudentDto.StudentId);
            cmd.Parameters.AddWithValue("course_id",courseStudentDto.CourseId);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}


