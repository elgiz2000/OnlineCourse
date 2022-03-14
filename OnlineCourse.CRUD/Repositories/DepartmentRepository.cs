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
            cmd.CommandText = "Select department.id,department.name,course.id,course.name,course.price from department left join course on department.id=course.department_id";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var departmentId = Convert.ToInt32(reader.GetValue(0));
                var departmentName = reader.GetValue(1).ToString();
                var department = departments.Where(p => p.Id == departmentId).FirstOrDefault();
                if (reader.GetValue(2) == DBNull.Value)
                {
                    department = new();
                    department.Id = departmentId;
                    department.Name = departmentName;
                    department.Courses = new();
                    departments.Add(department);
                }
                else
                {


                    var courseId = Convert.ToInt32(reader.GetValue(2));
                    var courseName = reader.GetValue(3).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(4));

                    if (department == null)
                    {
                        department = new();
                        department.Id = departmentId;
                        department.Name = departmentName;
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        department.Courses = new();
                        department.Courses.Add(course);
                        departments.Add(department);
                    }
                    else
                    {
                        Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                        if (department.Courses != null)
                        {
                            if (!department.Courses.Contains(course))
                                department.Courses.Add(course);
                        };

                    }
                }

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
            cmd.CommandText = "Select department.id,department.name,course.id,course.name,course.price from department left join course on department.id=course.department_id where department.id=@id";
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            dpt.Courses = new();
            while (reader.Read())
            {
                var departmentId = Convert.ToInt32(reader.GetValue(0));
                var departmentName = reader.GetValue(1).ToString();
                if (reader.GetValue(2) == DBNull.Value)
                {
                    dpt = new();
                    dpt.Id = departmentId;
                    dpt.Name = departmentName;
                    dpt.Courses = new();
                }
                else
                {


                    var courseId = Convert.ToInt32(reader.GetValue(2));
                    var courseName = reader.GetValue(3).ToString();
                    var coursePrice = Convert.ToInt32(reader.GetValue(4));
                    dpt.Id = departmentId;
                    dpt.Name = departmentName;
                    Course course = new() { Id = courseId, Name = courseName, Price = coursePrice };
                    dpt.Courses.Add(course);
                }
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
            cmd.CommandText = "Delete from department where id=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void AddDepartment(Department department)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "Insert into department(name) VALUES(@name)";
            if (department.Name != null) cmd.Parameters.AddWithValue("name", department.Name);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateDepartment(Department department)
        {
            con.Open();
            NpgsqlCommand cmd = new();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE department SET name = @name WHERE id = @id";
            if (department.Name != null) cmd.Parameters.AddWithValue("name", department.Name);
            cmd.Parameters.AddWithValue("id", department.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


