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
        readonly NpgsqlConnection con = new (connectionString);
        public List<Student> GetStudents()
        {
            con.Open();
            List<Student> students = new ();
            NpgsqlCommand cmd = new ();
            cmd.Connection = con;
            cmd.CommandText = "Select * from students";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
              Student std = new();
              std.Id = Convert.ToInt32(reader.GetValue(0));
              std.Name = reader.GetValue(1).ToString();
              students.Add(std);
            }
             reader.Close();
             con.Close();
             return students;
            }
        public Student GetStudentById(int id)
        {
            con.Open();
            Student student = new ();
            NpgsqlCommand cmd = new ();
            cmd.Connection = con;
            cmd.CommandText= "Select * from students where id=" + id + "";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                student.Id = Convert.ToInt32(reader.GetValue(0));
                student.Name = reader.GetValue(1).ToString();
            }
            reader.Close();
            con.Close ();   
            return student;
        }

        public void DeleteStudent(int id)
        {
            con.Open();
            NpgsqlCommand cmd=new ();
            cmd.Connection=con;
            cmd.CommandText = "Delete from students where id=" + id + "";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddStudent(Student student)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText=String.Format("insert into students(name) VALUES('{0}')",student.Name);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void UpdateStudent(Student student) {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = String.Format("UPDATE students SET name = '{0}' WHERE id = '{1}'", student.Name, student.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


