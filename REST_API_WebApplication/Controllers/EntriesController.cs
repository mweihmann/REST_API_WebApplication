using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using REST_API_WebApplication.Models;
using System.Data;
using System.Data.SqlClient;

namespace REST_API_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public EntriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get methode
        [HttpGet]
        public JsonResult Get()
        {
            //select statement in query variable
            string query = @"
                            select Id, Description from
                            dbo.Entry";

            // table verweist auf den memory table
            DataTable table = new DataTable();

            // connectionstring in appsettings.json wird in sqldatasource variable geschrieben
            string sqlDataSource = _configuration.GetConnectionString("RestAPIDatabase");

            // reader um rows einer sqldb zu lesen
            SqlDataReader myReader;

            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                // öffnen der verbindung zur sql db
                myConn.Open();

                // sql statement von query + connection werden gegen die sql db ausgeführt bzw in die variable cmd geschrieben
                using (SqlCommand cmd = new SqlCommand(query, myConn))
                {
                    // sql statement + connection werden mit der executereader methode auf die variable myreader verwiesen.
                    myReader = cmd.ExecuteReader();
                    // in memory table läd den inhalt von myreader
                    table.Load(myReader);

                    //reader und verbindung schließen
                    myReader.Close();
                    myConn.Close();
                }
            }
            // returned jsonresult der variable table
            return new JsonResult(table);
        }


        [HttpGet("{id}")]
        public JsonResult GetById(int id)
        {
            //select statement in query variable
            string query = @"
                            select * from dbo.Entry
                            where Id=@Id";

            // table verweist auf den memory table
            DataTable table = new DataTable();

            // connectionstring in appsettings.json wird in sqldatasource variable geschrieben
            string sqlDataSource = _configuration.GetConnectionString("RestAPIDatabase");

            // reader um rows einer sqldb zu lesen
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                // öffnen der verbindung zur sql db
                myConn.Open();

                // sql statement von query + connection werden in die variable cmd geschrieben
                using (SqlCommand cmd = new SqlCommand(query, myConn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    // sql statement + connection werden mit der executereader methode auf die variable myreader verwiesen.
                    myReader = cmd.ExecuteReader();
                    // in memory table läd den inhalt von myreader
                    table.Load(myReader);

                    //reader und verbindung schließen
                    myReader.Close();
                    myConn.Close();
                }
            }
            // returned jsonresult der variable table
            return new JsonResult(table);
        }



        [HttpPost]
        public JsonResult Post(Entry entry)
        {
            //select statement in query variable
            string query = @"
                            insert into dbo.Entry
                            values (@Description)";

            // table verweist auf den memory table
            DataTable table = new DataTable();

            // connectionstring in appsettings.json wird in sqldatasource variable geschrieben
            string sqlDataSource = _configuration.GetConnectionString("RestAPIDatabase");

            // reader um rows einer sqldb zu lesen
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                // öffnen der verbindung zur sql db
                myConn.Open();

                // sql statement von query + connection werden in die variable cmd geschrieben
                using (SqlCommand cmd = new SqlCommand(query, myConn))
                {
                    //sql statement von query + connection werden gegen die sql db ausgeführt mit den parametern um in description zu schreiben
                    cmd.Parameters.AddWithValue("@Description", entry.Description);
                    // sql statement + connection werden mit der executereader methode auf die variable myreader verwiesen.
                    myReader = cmd.ExecuteReader();
                    // in memory table läd den inhalt von myreader
                    table.Load(myReader);

                    //reader und verbindung schließen
                    myReader.Close();
                    myConn.Close();
                }
            }
            // returned jsonresult der variable table
            return new JsonResult("Added successful");
        }


        [HttpPut]
        public JsonResult Put(Entry entry)
        {
            //select statement in query variable
            string query = @"
                            update dbo.Entry
                            set Description= @Description
                            where Id=@Id";

            // table verweist auf den memory table
            DataTable table = new DataTable();

            // connectionstring in appsettings.json wird in sqldatasource variable geschrieben
            string sqlDataSource = _configuration.GetConnectionString("RestAPIDatabase");

            // reader um rows einer sqldb zu lesen
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                // öffnen der verbindung zur sql db
                myConn.Open();

                // sql statement von query + connection werden in die variable cmd geschrieben
                using (SqlCommand cmd = new SqlCommand(query, myConn))
                {
                    cmd.Parameters.AddWithValue("@Id", entry.Id);
                    //sql statement von query + connection werden gegen die sql db ausgeführt mit den parametern um in description zu schreiben
                    cmd.Parameters.AddWithValue("@Description", entry.Description);
                    // sql statement + connection werden mit der executereader methode auf die variable myreader verwiesen.
                    myReader = cmd.ExecuteReader();
                    // in memory table läd den inhalt von myreader
                    table.Load(myReader);

                    //reader und verbindung schließen
                    myReader.Close();
                    myConn.Close();
                }
            }
            // returned jsonresult der variable table
            return new JsonResult("Updated successful");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //select statement in query variable
            string query = @"
                            delete from dbo.Entry
                            where Id=@Id";

            // table verweist auf den memory table
            DataTable table = new DataTable();

            // connectionstring in appsettings.json wird in sqldatasource variable geschrieben
            string sqlDataSource = _configuration.GetConnectionString("RestAPIDatabase");

            // reader um rows einer sqldb zu lesen
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                // öffnen der verbindung zur sql db
                myConn.Open();

                // sql statement von query + connection werden in die variable cmd geschrieben
                using (SqlCommand cmd = new SqlCommand(query, myConn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    
                    // sql statement + connection werden mit der executereader methode auf die variable myreader verwiesen.
                    myReader = cmd.ExecuteReader();
                    // in memory table läd den inhalt von myreader
                    table.Load(myReader);

                    //reader und verbindung schließen
                    myReader.Close();
                    myConn.Close();
                }
            }
            // returned jsonresult der variable table
            return new JsonResult("Deleted successful");
        }

    }
}
