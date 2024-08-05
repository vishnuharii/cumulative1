using Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {   
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Teacher> ListTeacher()
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = (int)ResultSet["teacherid"];
                string TfName = ResultSet["teacherfname"].ToString();
                string TlName = ResultSet["teacherlname"].ToString();
                string Tnumber = ResultSet["employeenumber"].ToString();
                DateTime Thiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Tsalary = Convert.ToDecimal(ResultSet["salary"]);



                Teacher NewTeacher = new Teacher();
                NewTeacher.Id = Id;
                NewTeacher.TfName = TfName;
                NewTeacher.TlName = TlName;
                NewTeacher.Tnumber = Tnumber;
                NewTeacher.Thiredate = Thiredate;
                NewTeacher.Tsalary = Tsalary;

                //Add the Author Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;
        }


        /// <summary>
        /// search details of a teacher by id 
        /// </summary>
      
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = (int)ResultSet["teacherid"];
                string TfName = ResultSet["teacherfname"].ToString();
                string TlName = ResultSet["teacherlname"].ToString();
                string Tnumber = ResultSet["employeeteacher"].ToString();
                DateTime Thiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Tsalary = Convert.ToDecimal(ResultSet["salary"]);



                NewTeacher.Id = Id;
                NewTeacher.TfName = TfName;
                NewTeacher.TlName = TlName;
                NewTeacher.Tnumber = Tnumber;
                NewTeacher.Thiredate = Thiredate;
                NewTeacher.Tsalary = Tsalary;
            }


            return NewTeacher;
        }
    }
}
