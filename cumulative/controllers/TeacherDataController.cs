using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourNamespace.Models;
using MySql.Data.MySqlClient;

namespace YourNamespace.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext _context = new SchoolDbContext();

        /// <summary>
        /// Returns a list of teachers in the system.
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>A list of teachers</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            // Create an instance of a connection
            MySqlConnection conn = _context.AccessDatabase();

            // Open the connection between the web server and database
            conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "SELECT * FROM Teachers";

            // Gather Result Set of Query into a variable
            MySqlDataReader resultSet = cmd.ExecuteReader();

            // Create an empty list of teachers
            List<Teacher> teachers = new List<Teacher>();

            // Loop Through Each Row of the Result Set
            while (resultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int teacherId = (int)resultSet["TeacherId"];
                string name = resultSet["Name"].ToString();
                DateTime hireDate = (DateTime)resultSet["HireDate"];
                decimal salary = (decimal)resultSet["Salary"];

                Teacher newTeacher = new Teacher
                {
                    TeacherId = teacherId,
                    Name = name,
                    HireDate = hireDate,
                    Salary = salary
                };

                // Add the teacher to the list
                teachers.Add(newTeacher);
            }

            // Close the connection between the MySQL Database and the Web Server
            conn.Close();

            // Return the final list of teachers
            return teachers;
        }

        /// <summary>
        /// Finds a teacher in the system given an ID.
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public IHttpActionResult FindTeacher(int id)
        {
            Teacher teacher = null;

            // Create an instance of a connection
            MySqlConnection conn = _context.AccessDatabase();

            // Open the connection between the web server and database
            conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "SELECT * FROM Teachers WHERE TeacherId = @id";
            cmd.Parameters.AddWithValue("@id", id);

            // Gather Result Set of Query into a variable
            MySqlDataReader resultSet = cmd.ExecuteReader();

            if (resultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int teacherId = (int)resultSet["TeacherId"];
                string name = resultSet["Name"].ToString();
                DateTime hireDate = (DateTime)resultSet["HireDate"];
                decimal salary = (decimal)resultSet["Salary"];

                teacher = new Teacher
                {
                    TeacherId = teacherId,
                    Name = name,
                    HireDate = hireDate,
                    Salary = salary
                };
            }

            // Close the connection
            conn.Close();

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
    }
}
