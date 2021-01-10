using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using API_Practice.Models;
using Newtonsoft.Json;

namespace API_Practice.Controllers
{
    public class EmployeeController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
       
        [HttpPost] //always call when the button is click
        public void insertData([FromBody]Emp _emp) // frombody is use to tell the it is been called from the body 
        {
            con.Open();
            SqlCommand com = new SqlCommand("SP_EMP_INSERT", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@name", _emp.Ename);
            com.Parameters.AddWithValue("@age", _emp.age);
            com.ExecuteNonQuery();
            con.Close();
        }

        [HttpGet] // called for getting the data , and no need to mention also because it will always called by default
        public string getdata()
        {
            string data = "";
            con.Open();
            SqlCommand com = new SqlCommand("",con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return data;
        }
    }
}
