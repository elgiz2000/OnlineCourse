using Npgsql;
using OnlineCourse.Data;
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
        public List<Course> GetCourses()
        {
            con.Open();
            List<Course> courses = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from courses";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Course crs = new();
                crs.Id = Convert.ToInt32(reader.GetValue(0));
                crs.Name = reader.GetValue(1).ToString();
                crs.Price = Convert.ToInt32(reader.GetValue(2));
                crs.TeacherId = Convert.ToInt32(reader.GetValue(3));
                crs.DepartmentId = Convert.ToInt32(reader.GetValue(4));
                courses.Add(crs);
            }
            reader.Close();
            con.Close();
            return courses;
        }
        public Course GetCourseById(int id)
        {
            con.Open();
            Course crs = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from courses where id=" + id + "";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                crs.Id = Convert.ToInt32(reader.GetValue(0));
                crs.Name = reader.GetValue(1).ToString();
                crs.Price = Convert.ToInt32(reader.GetValue(2));
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
            cmd.CommandText = "Delete from courses where id=" + id + "";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = String.Format("insert into courses(name,price,teacherid,departmentid) VALUES('{0}','{1}','{2}','{3}')", course.Name, course.Price,course.TeacherId,course.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateCourse(Course course)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = String.Format("UPDATE courses SET name = '{0}',price=" + course.Price + " WHERE id = '{1}'", course.Name, course.Id); ;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


