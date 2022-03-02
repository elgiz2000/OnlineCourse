using Npgsql;
using OnlineCourse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.CRUD.Repositories
{
    public class DepartmentRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=test;User Id=postgres;Password=root;";
        readonly NpgsqlConnection con = new(connectionString);
        public List<Department> GetDepartments()
        {
            con.Open();
            List<Department> departments = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from departments";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Department dpt = new();
                dpt.Id = Convert.ToInt32(reader.GetValue(0));
                dpt.Name = reader.GetValue(1).ToString();
                departments.Add(dpt);
            }
            reader.Close();
            con.Close();
            return departments;
        }
        public Department GetDepartmentById(int id)
        {
            con.Open();
            Department dpt = new();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Select * from departments where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dpt.Id = Convert.ToInt32(reader.GetValue(0));
                dpt.Name = reader.GetValue(1).ToString();
            }
            reader.Close();
            con.Close();
            return dpt;
        }

        public void DeleteDepartment(int id)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Delete from departments where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddDepartment(Department department)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into departments(name) VALUES(@name)";
            cmd.Parameters.AddWithValue("name", department.Name);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateDepartment(Department department)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE departments SET name = @name WHERE id = @id";
            cmd.Parameters.AddWithValue("name", department.Name);
            cmd.Parameters.AddWithValue("id", department.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


