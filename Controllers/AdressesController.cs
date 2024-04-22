using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AdressesAPI.Models;
using System.Reflection.PortableExecutable;

namespace AdressesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : Controller
    {
        
        public AdressesController(IConfiguration configuration) 
        {
            Configuration = configuration;
            connectionString = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        private IConfiguration Configuration;
        SqlConnection connectionString;
        SqlCommand command;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;

        [HttpGet]
        public JsonResult GetAll()
        {
           
            dataTable = new DataTable();
            command = new SqlCommand("select * from Adresses", connectionString);
            connectionString.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);


            connectionString.Close();
            return Json(dataTable);
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Address item)
        {
           
            connectionString.Open();

            using (SqlCommand command = new SqlCommand(@"insert into Adresses (StudentId, Name, City, Country, PostalCode, ProvinceState, StreetName, StreetNumber)
	            values
                (@StudentId, @Name, @City, @Country, @PostalCode, @ProvinceState, @StreetName, @StreetNumber)
                ", connectionString))
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {

                    new SqlParameter() { ParameterName = "@StudentId", SqlDbType = SqlDbType.Int, Value = item.StudentId },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = item.Name },
                    new SqlParameter() { ParameterName = "@City", SqlDbType = SqlDbType.NVarChar, Value = item.City },
                    new SqlParameter() { ParameterName = "@Country", SqlDbType = SqlDbType.NVarChar, Value = item.Country },
                    new SqlParameter() { ParameterName = "@PostalCode", SqlDbType = SqlDbType.NVarChar, Value = item.PostalCode },
                    new SqlParameter() { ParameterName = "@ProvinceState", SqlDbType = SqlDbType.NVarChar, Value = item.ProvinceState },
                    new SqlParameter() { ParameterName = "@StreetName", SqlDbType = SqlDbType.NVarChar, Value = item.StreetName },
                    new SqlParameter() { ParameterName = "@StreetNumber", SqlDbType = SqlDbType.NVarChar, Value = item.StreetNumber },
                    
                };

                

                command.Parameters.AddRange(parameters.ToArray());

                command.ExecuteNonQuery();

          
            }

            connectionString.Close();

            return Ok(new {Message = "Record Added"});
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public JsonResult Read(int id)
        {
            
    

            SqlParameter parameters = new SqlParameter { ParameterName = "@AddressId", SqlDbType = SqlDbType.Int, Value = id };
            command = new SqlCommand("select * from Adresses where AdressId=@AddressId", connectionString);

            dataTable = new DataTable();

            try
            {
                connectionString.Open();

                command.Parameters.Add(parameters);

                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return Json(dataTable);
                }
                else
                {
                    return Json("Not Found");
                }

            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            finally
            {
                connectionString.Close();
            }
        }


        [Route("Edit")]
        [HttpPut] 
        public ActionResult Edit(int AddressId, int StudentId, string Name, string StreetName, string StreetNumber)
        {
            try
            {
               
                command = new SqlCommand(@"update Adresses set 
                                            [StudentId] = @StudentId, 
                                            [Name] = @Name,
                                            [StreetName] = @StreetName,
                                            [StreetNumber] = @StreeTNumber           
                                        where AdressId=@AddressId", connectionString);
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@AddressId", SqlDbType = SqlDbType.Int, Value = AddressId },
                    new SqlParameter() { ParameterName = "@StudentId", SqlDbType = SqlDbType.Int, Value = StudentId },
                    new SqlParameter() { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = Name },
                    new SqlParameter() { ParameterName = "@StreetName", SqlDbType = SqlDbType.NVarChar, Value = StreetName },
                    new SqlParameter() { ParameterName = "@StreetNumber", SqlDbType = SqlDbType.NVarChar, Value = StreetNumber },

                };
                connectionString.Open();

                command.Parameters.AddRange(parameters.ToArray());

                int effectedRows = command.ExecuteNonQuery();
                if (effectedRows > 0)
                {
                    return Ok(new { Message = "Record Updated" });
                }
                return BadRequest(new { Message = "Record Not found!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally { connectionString.Close(); }
        }


        [Route("Delete/{id}")]
        [HttpDelete] 
        public ActionResult Delete(int id)
        {
            try
            {        

                SqlParameter parameters = new SqlParameter { ParameterName = "@AddressId", SqlDbType = SqlDbType.Int, Value = id };
                

                command = new SqlCommand("delete from Adresses where AdressId=@AddressId", connectionString);
                connectionString.Open();
                command.Parameters.Add(parameters);
                int rowsEffected = command.ExecuteNonQuery();
                if (rowsEffected > 0)
                {
                    return Ok(new { Message = "Record Deleted!" });
                }
                return BadRequest(new { Message = "Record Not found!" });
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connectionString.Close(); }
        }
    }
}
