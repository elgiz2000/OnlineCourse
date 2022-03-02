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
            cmd.CommandText = "Select * from courses inner join departments on courses.departmentid=departments.id";
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
            cmd.CommandText = "Select * from courses inner join departments on courses.departmentid=departments.id where courses.id=@id";
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
            cmd.CommandText = "Delete from courses where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into courses(name,price,teacherid,departmentid) VALUES(@name,@price,@teacherid,@departmentid)";
            cmd.Parameters.AddWithValue("price", course.Price);
            cmd.Parameters.AddWithValue("name", course.Name);
            cmd.Parameters.AddWithValue("teacherid", course.TeacherId);
            cmd.Parameters.AddWithValue("departmentid", course.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            //cmd.Parameters
            cmd.CommandText ="UPDATE courses SET name = @name,price=@price,teacherid=@teacherid,departmentid=@departmentid WHERE id = @id";
            cmd.Parameters.AddWithValue("name", course.Name);
            cmd.Parameters.AddWithValue("price", course.Price);
            cmd.Parameters.AddWithValue("id", course.Id);
            cmd.Parameters.AddWithValue("teacherid",course.TeacherId);
            cmd.Parameters.AddWithValue("departmentid",course.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


