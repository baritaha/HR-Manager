using compantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace compantAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string Query = @"
                            SELECT EmployeeId, [EmployeeName],DepartmentPart,convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFileName,EmpPassword,EmpCPassword,GenderType FROM [Company].[dbo].[Employee]
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpPost]
        public JsonResult AddEmployee(Employee Emp)
        {
            string Query = @"
                           insert into  [Company].[dbo].[Employee]
                            (EmployeeName,DepartmentPart, DateOfJoining,PhotoFileName)
                            values(@EmployeeName,@DepartmentPart,@DateOfJoining,@PhotoFileName)
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {

                    DateTime dateOfJoining = DateTime.Now;
                    MyCommand.Parameters.AddWithValue("@EmployeeName", Emp.EmployeeName);
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", Emp.DepartmentPart);
                    MyCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                    MyCommand.Parameters.AddWithValue("@PhotoFileName", Emp.PhotoFileName);

                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Added Successfull");

        }
        [HttpPut]
        public JsonResult UpdateDepartment(Employee Emp)
        {
            string Query = @"
                           update [Company].[dbo].[Employee]
                            set EmployeeName=@EmployeeName ,
                            DepartmentPart=@DepartmentPart,
                            DateOfJoining=@DateOfJoining,
                            PhotoFileName=@PhotoFileName,
                            EmpPassword=@EmpPassword,
                            EmpCPassword=@EmpCPassword
                            where EmployeeId=@EmployeeId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@EmployeeId", Emp.EmployeeId);
                    MyCommand.Parameters.AddWithValue("@EmployeeName", Emp.EmployeeName);
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", Emp.DepartmentPart);
                    MyCommand.Parameters.AddWithValue("@DateOfJoining", Emp.DateOfJoining);
                    MyCommand.Parameters.AddWithValue("@PhotoFileName", Emp.PhotoFileName);
                    MyCommand.Parameters.AddWithValue("@EmpPassword", Emp.EmpPassword);
                    MyCommand.Parameters.AddWithValue("@EmpCPassword", Emp.EmpCPassword);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Updated Successfull");

        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string Query = @"
                           delete from  [Company].[dbo].[Employee]
                            where EmployeeId=@EmployeeId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@EmployeeId", id);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Deleted Successfull");

        }
        [HttpDelete("DeleteRequest")]
        public JsonResult DeleteRequest(int EmployeeId, int  RequestId)
        {
            string Query = @"
                           delete from  [Company].[dbo].[Vacation]
                            where EmployeeId=@EmployeeId AND RequestId=@RequestId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    MyCommand.Parameters.AddWithValue("@RequestId", RequestId);

                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Deleted Successfull");

        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var phisicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using (var stream = new FileStream(phisicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);

            }
            catch (Exception)
            {
                return new JsonResult("picture1.png");
            }

        }
        [HttpPost]
        [Route("api/Registing")]
        public JsonResult Registing(Employee emp)
        {
            if (emp.EmpPassword != emp.EmpCPassword)
            {
                return new JsonResult("Passwords don't match");
            }
            string Query = @"
                           insert into  [Company].[dbo].[Employee]
                            (EmployeeName,DepartmentPart, DateOfJoining,PhotoFileName,EmpPassword,EmpCPassword,GenderType)
                            values(@EmployeeName,@DepartmentPart,@DateOfJoining,@PhotoFileName,@EmpPassword,@EmpCPassword,@GenderType)
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {

                    DateTime dateOfJoining = DateTime.Now;
                    MyCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", emp.DepartmentPart);
                    MyCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    MyCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    MyCommand.Parameters.AddWithValue("@EmpPassword", emp.EmpPassword);
                    MyCommand.Parameters.AddWithValue("@EmpCPassword", emp.EmpCPassword);
                    MyCommand.Parameters.AddWithValue("@GenderType", emp.GenderType);


                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Added Successfull");
        }
        [HttpGet("{EmployeeName}")]
    
        public JsonResult GetDataByName( string EmployeeName)
        {
            string Query = @"
                            SELECT EmployeeId,EmployeeName,DepartmentPart,PhotoFileName,GenderType FROM [Company].[dbo].[Employee]
                            where EmployeeName=@EmployeeName
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);
        }
       
        [HttpPost]
        [Route("api/Login")]
        public IActionResult Login(loginEmployee emp)
        {
            string query = "SELECT COUNT(*) FROM Employee WHERE EmployeeName = @EmployeeName AND EmpPassword = @EmpPassword" ;
            int result = 0;
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@EmpPassword", emp.EmpPassword);
                   
                    result = (int)myCommand.ExecuteScalar();
                }
            }

            if (result > 0)
            {
               
                var token = GenerateJWTToken(emp.EmployeeName, emp.EmpPassword,emp.EmployeeId,emp.GenderType);
                return new JsonResult(new { message = "Login Successful", token });
            }
            else
            {
              
                return new JsonResult(new { message = "Login Failed" });
            }
        }
        private string GenerateJWTToken(string employeeName, string empPassword,int EmployeeId,string GenderType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtExpireMinutes = _configuration.GetValue<int>("JwtSettings:JwtExpireMinutes");

            var claims = new List<Claim>
            {
               new Claim("EmployeeName", employeeName),
        new Claim("EmpPassword", empPassword),
        new Claim("EmployeeId", EmployeeId.ToString()),
        new Claim("GenderType",GenderType),

            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:ValidIssuer"],
                _configuration["JwtSettings:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(jwtExpireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        [Route("api/RequestVacation")]
        public IActionResult RequestVacation([FromBody] VacationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid vacation request data.");
            }

            if (request.StartDate >= request.EndDate)
            {
                return BadRequest("Invalid date range for vacation request.");
            }

            string connectionString = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string overlapQuery = "SELECT COUNT(*) FROM Vacation WHERE (@StartDate <= EndDate) AND (@EndDate >= StartDate)";
                using (SqlCommand overlapCommand = new SqlCommand(overlapQuery, connection))
                {
                    overlapCommand.Parameters.AddWithValue("@StartDate", request.StartDate);
                    overlapCommand.Parameters.AddWithValue("@EndDate", request.EndDate);

                    int overlapCount = (int)overlapCommand.ExecuteScalar();

                    if (overlapCount > 0)
                    {
                        return BadRequest("Vacation request conflicts with an existing vacation.");
                    }
                }


                // Insert the vacation request
                string insertQuery = "INSERT INTO Vacation (EmployeeId, StartDate, EndDate, Status,EmployeeName,DepartmentPart,GenderType) VALUES (@EmployeeId, @StartDate, @EndDate, @Status,@EmployeeName,@DepartmentPart,@GenderType)";


                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                    command.Parameters.AddWithValue("@StartDate", request.StartDate);
                    command.Parameters.AddWithValue("@EndDate", request.EndDate);
                    command.Parameters.AddWithValue("@Status", "Pending");
                    command.Parameters.AddWithValue("@EmployeeName", request.EmployeeName);
                    command.Parameters.AddWithValue("@DepartmentPart", request.DepartmentPart);
                    command.Parameters.AddWithValue("@GenderType", request.GenderType);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        return Ok("Vacation request submitted successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to submit vacation request.");
                    }
                }

            }
        }
        [HttpPost]
        [Route("api/RequestDepartures")]
        public IActionResult RequestDepartures([FromBody] DeparturesRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid vacation request data.");
            }

            if (request.StartHoure >= request.EndHoure)
            {
                return BadRequest("Invalid date range for Departure request.");
            }

            string connectionString = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string overlapQuery = "SELECT COUNT(*) FROM Departure WHERE (@StartHoure <= EndHoure) AND (@EndHoure >= StartHoure)";
                using (SqlCommand overlapCommand = new SqlCommand(overlapQuery, connection))
                {
                    overlapCommand.Parameters.AddWithValue("@StartHoure", request.StartHoure);
                    overlapCommand.Parameters.AddWithValue("@EndHoure", request.EndHoure);

                    int overlapCount = (int)overlapCommand.ExecuteScalar();

                    if (overlapCount > 0)
                    {
                        return BadRequest("Departure request conflicts with an existing Departure.");
                    }
                }


                // Insert the vacation request
                string insertQuery = "INSERT INTO Departure (EmployeeId, StartHoure, EndHoure, Status,EmployeeName,DepartmentPart) VALUES (@EmployeeId, @StartHoure, @EndHoure, @Status,@EmployeeName,@DepartmentPart)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                    command.Parameters.AddWithValue("@StartHoure", request.StartHoure);
                    command.Parameters.AddWithValue("@EndHoure", request.EndHoure);
                    command.Parameters.AddWithValue("@Status", "Pending");
                    command.Parameters.AddWithValue("@EmployeeName", request.EmployeeName);
                    command.Parameters.AddWithValue("@DepartmentPart", request.DepartmentPart);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        return Ok("Departure request submitted successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to submit Departure request.");
                    }
                }

            }
        }
        [HttpGet("GetRequests")]
        public JsonResult GetRequests()
        {
            string Query = @"
                            SELECT EmployeeId, StartDate, EndDate, Status,EmployeeName,DepartmentPart FROM [Company].[dbo].[Vacation]
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }

        [HttpGet("GetRequestsDep")]
        public JsonResult GetRequestsDep()
        {
            string Query = @"
                            SELECT EmployeeId, StartHoure, EndHoure, Status,EmployeeName,DepartmentPart FROM [Company].[dbo].[Departure]
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpPost]
        [Route("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] VacationStatusUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Retrieve the EmployeeId and Status from the request
            int employeeId = request.EmployeeId;
            string newStatus = request.Status;

            string connectionString = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the status for the specific employee and date range
                string updateQuery = @"
            UPDATE Vacation 
            SET Status = @newStatus
            WHERE EmployeeId = @EmployeeId
            AND StartDate = @StartDate
            AND EndDate = @EndDate
        ";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@NewStatus", newStatus);
                    command.Parameters.AddWithValue("@StartDate", request.StartDate);
                    command.Parameters.AddWithValue("@EndDate", request.EndDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        return Ok("Status updated successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to update status.");
                    }
                }
            }
        }
        [HttpPost]
        [Route("UpdateStatusDep")]
        public IActionResult UpdateStatusDep([FromBody] DepartureStatusUpdate request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Retrieve the EmployeeId and Status from the request
            int employeeId = request.EmployeeId;
            string newStatus = request.Status;

            string connectionString = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Update the status for the specific employee and date range
                string updateQuery = @"
            UPDATE Departure 
            SET Status = @newStatus
            WHERE EmployeeId = @EmployeeId
            AND StartHoure = @StartHoure
            AND EndHoure = @EndHoure
        ";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@NewStatus", newStatus);
                    command.Parameters.AddWithValue("@StartHoure", request.StartHoure);
                    command.Parameters.AddWithValue("@EndHoure", request.EndHoure);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        return Ok("Status updated successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to update status.");
                    }
                }
            }
        }
        [HttpGet("GetVacationbyDepName")]
        public JsonResult GetVacationbyDepName( string DepartmentPart)
        {
            string Query = @"
                            SELECT RequestId,EmployeeName,StartDate,EndDate,Status,EmployeeId,GenderType FROM [Company].[dbo].[Vacation]
                            where DepartmentPart=@DepartmentPart
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", DepartmentPart);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpGet("GetDeparturebyDepName")]
        public JsonResult GetDeparturebyDepName(string DepartmentPart)
        {
            string Query = @"
                            SELECT EmployeeName,StartHoure,EndHoure,Status,EmployeeId FROM [Company].[dbo].[Departure]
                            where DepartmentPart=@DepartmentPart
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", DepartmentPart);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpGet("GetEmployeeVacations")]
        public JsonResult GetEmployeeVacations(int EmployeeId)
        {
            string Query = @"
                            SELECT EmployeeId,DepartmentPart, StartDate,EndDate,Status,EmployeeName FROM [Company].[dbo].[Vacation]
                            where EmployeeId=@EmployeeId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpGet("GetEmployeeDeparture")]
        public JsonResult GetEmployeeDeparture(int EmployeeId)
        {
            string Query = @"
                            SELECT EmployeeId,DepartmentPart, StartHoure,EndHoure,Status,EmployeeName FROM [Company].[dbo].[Departure]
                            where EmployeeId=@EmployeeId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    
                    MyCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);

        }
        [HttpPost("AddSuggestion")]
        public JsonResult AddSuggestion(sugestion sug)
        {
            string Query = @"
                           insert into  [Company].[dbo].[Suggestions] 
                            values(@SuggestText,@EmployeeId,@EmployeeName,@DepartmentPart)
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@SuggestText", sug.SuggestText);
                    MyCommand.Parameters.AddWithValue("@EmployeeId", sug.EmployeeId);
                    MyCommand.Parameters.AddWithValue("@EmployeeName",sug.EmployeeName);
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", sug.DepartmentPart);


                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Added Suggestions Successfull");

        }
        [HttpGet("GetAllSuggestions")]
        public JsonResult GetAllSuggestions(string DepartmentPart)
        {
            string Query = @"
                            SELECT SuggestText,EmployeeName,DepartmentPart FROM [Company].[dbo].[Suggestions]
                            where DepartmentPart=@DepartmentPart
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentPart", DepartmentPart);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult(Table);
        }





    }

}


