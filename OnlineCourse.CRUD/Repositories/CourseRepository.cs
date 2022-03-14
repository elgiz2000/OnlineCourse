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
    public class CourseRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=root;";
        readonly NpgsqlConnection con = new(connectionString);
        public List<CourseGetDto> GetCourses()
        {
            con.Open();
            List<CourseGetDto> courses = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from course inner join department on course.department_id=department.id";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CourseGetDto crs = new();
                crs.Id = Convert.ToInt32(reader.GetValue(0));
                crs.Name = reader.GetValue(1).ToString();
                crs.Price = Convert.ToInt32(reader.GetValue(2));
                crs.DepartmentName = reader.GetValue(6).ToString();
                courses.Add(crs);
            }
            reader.Close();
            con.Close();
            return courses;
        }
        public CourseGetDto GetCourseById(int id)
        {
            con.Open();
            CourseGetDto crs = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from course inner join department on course.department_id=department.id where course.id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                crs.Id = Convert.ToInt32(reader.GetValue(0));
                crs.Name = reader.GetValue(1).ToString();
                crs.Price = Convert.ToInt32(reader.GetValue(2));
                crs.DepartmentName = reader.GetValue(6).ToString();
            }
            reader.Close();
            con.Close();
            return crs;
        }

        public void DeleteCourse(int id)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Delete from course where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into course(name,price,teacher_id,department_id) VALUES(@name,@price,@teacher_id,@department_id)";
            cmd.Parameters.AddWithValue("price", course.Price);
            if (course.Name != null) cmd.Parameters.AddWithValue("name", course.Name);
            cmd.Parameters.AddWithValue("teacher_id", course.TeacherId);
            cmd.Parameters.AddWithValue("department_id", course.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            //cmd.Parameters
            cmd.CommandText = "UPDATE course SET name = @name,price=@price,teacher_id=@teacher_id,department_id=@department_id WHERE id = @id";
            if (course.Name != null) cmd.Parameters.AddWithValue("name", course.Name);
            cmd.Parameters.AddWithValue("price", course.Price);
            cmd.Parameters.AddWithValue("id", course.Id);
            cmd.Parameters.AddWithValue("teacher_id", course.TeacherId);
            cmd.Parameters.AddWithValue("department_id", course.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


