using compantAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace compantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;


        }
        [HttpGet]
        public JsonResult Get()
        {
            string Query = @"
                            SELECT [DepartmentId],[DepartmentName],DepPassword FROM [Company].[dbo].[Department]
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
        [HttpGet("api/GetDepName")]
        public JsonResult GetDepName()
        {
            string Query = @"
                            SELECT [DepartmentName] FROM [Company].[dbo].[Department]
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
        public JsonResult AddDepartment(Department Dep)
        {
            string Query = @"
                           insert into  [Company].[dbo].[Department] 
                            values(@DepartmentName,@DepPassword)
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentName", Dep.DepartmentName);
                    MyCommand.Parameters.AddWithValue("@DepPassword", Dep.DepPassword);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Added Successfull");

        }
        [HttpPut]
        public JsonResult UpdateDepartment(Department Dep)
        {
            string Query = @"
                           update [Company].[dbo].[Department]
                            set DepartmentName=@DepartmentName ,DepPassword=@DepPassword
                            where DepartmentId=@DepartmentId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentId", Dep.DepartmentId);
                    MyCommand.Parameters.AddWithValue("@DepartmentName", Dep.DepartmentName);
                    MyCommand.Parameters.AddWithValue("@DepPassword", Dep.DepPassword);
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
                           delete from  [Company].[dbo].[Department]
                            where DepartmentId=@DepartmentId
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentId", id);
                    MyReader = MyCommand.ExecuteReader();
                    Table.Load(MyReader);
                    MyReader.Close();
                    MyCon.Close();
                }
            }
            return new JsonResult("Deleted Successfull");

        }
        [HttpPost("CheckLogin")]
        public JsonResult CheckLogin(LoginDepartment Dep)
        {
            string query = "SELECT COUNT(*) FROM Department WHERE DepartmentName = @DepartmentName AND DepPassword = @DepPassword";
            int result = 0;
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", Dep.DepartmentName);
                    myCommand.Parameters.AddWithValue("@DepPassword", Dep.DepPassword);
                    result = (int)myCommand.ExecuteScalar();
                }
            }

            if (result > 0)
            {

                var token = GenerateJWTToken(Dep.DepartmentName, Dep.DepPassword,Dep.DepartmentId);
                return new JsonResult(new { message = "Login Successful", token });
            }
            else
            {

                return new JsonResult(new { message = "Login Failed" });
            }
        }
        private string GenerateJWTToken(string departmentName, string depPassword,int departmentId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               new Claim("DepartmentName", departmentName),
        new Claim("DepPassword", depPassword),
        new Claim("DepartmentId", departmentId.ToString()),

            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:ValidIssuer"],
                _configuration["JwtSettings:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("{DepartmentName}")]

        public JsonResult DepartmentName(string DepartmentName)
        {
            string Query = @"
                            SELECT DepartmentId,DepartmentName FROM [Company].[dbo].[Department]
                            where DepartmentName=@DepartmentName
                ";
            DataTable Table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyAppCon");
            SqlDataReader MyReader;
            using (SqlConnection MyCon = new SqlConnection(sqlDataSource))
            {
                MyCon.Open();
                using (SqlCommand MyCommand = new SqlCommand(Query, MyCon))
                {
                    MyCommand.Parameters.AddWithValue("@DepartmentName", DepartmentName);
       
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
