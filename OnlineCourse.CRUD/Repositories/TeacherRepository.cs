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
            cmd.CommandText = "Select * from teachers";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Teacher teacher = new();
                teacher.Id = Convert.ToInt32(reader.GetValue(0));
                teacher.Salary = Convert.ToInt32(reader.GetValue(1));
                teacher.Name = reader.GetValue(2).ToString();
                teacher.Email = reader.GetValue(3).ToString();
                teachers.Add(teacher);
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
            cmd.CommandText = "Select * from teachers where id=" + id + "";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                teacher.Id = Convert.ToInt32(reader.GetValue(0));
                teacher.Salary = Convert.ToInt32(reader.GetValue(1));
                teacher.Name = reader.GetValue(2).ToString();
                teacher.Email = reader.GetValue(3).ToString();
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
            cmd.CommandText = "Delete from teachers where id=" + id + "";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddTeacher(Teacher teacher)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = String.Format("insert into teachers (salary,name,email) VALUES('{0}','{1}','{2}')", teacher.Salary,teacher.Name,teacher.Email);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateTeacher(Teacher teacher)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = String.Format("UPDATE teachers SET name = '{0}',salary="+teacher.Salary+",email='{1}' WHERE id = '{2}'", teacher.Name,teacher.Email,teacher.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


