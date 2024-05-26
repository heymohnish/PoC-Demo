using PoC_Demo.Model;
using PoC_Demo.Repository.Interface;
using System.Data.SqlClient;
using System.Data;
using OfficeOpenXml;
using System.Linq;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace PoC_Demo.Repository
{
    public class ProductRepository :Connection,IProductRepository
    {
        protected readonly IConfiguration Configuration;

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPI_AddProduct", SqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    cmd.Parameters.AddWithValue("@ProductCount", product.ProductCount);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected >= 1;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPS_GetProducts", SqlConnection))
                {
                    List<Product> productslist = new List<Product>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader =await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            productslist.Add(
                            new Product
                            {
                                ProductId= Convert.ToInt32(reader["ID"]),
                                ProductName = Convert.ToString(reader["ProductName"]),
                                ProductDescription = Convert.ToString(reader["ProductDescription"]),
                                ProductCount = Convert.ToInt32(reader["ProductCount"]),

                            });
                        }
                    }
                    return productslist;
                }
            }
            finally
            {
                CloseConnection();
            }
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPU_UpdateProduct", SqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    cmd.Parameters.AddWithValue("@ProductCount", product.ProductCount);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected >= 1;
                }
            }
            finally
            {
                CloseConnection();
            }
        }


        public async Task<bool> RemoveProduct(int id)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPD_RemoveProduct", SqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected >= 1;
                }
            }
            finally
            {
                CloseConnection();
            }
        }


        public async Task<bool> ValidateUser(Login login)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPS_ValidateUser", SqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username",login.Username);
                    cmd.Parameters.AddWithValue("@Password", login.Password);
                 
                    using (SqlDataReader reader =await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand("GetUserByEmailAndPassword", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            return new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.GetString(reader.GetOrdinal("phone")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("created_date")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("modified_date"))
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
            }
            catch (Exception ex)
            {
                // Log or handle exception
                Console.WriteLine("Error fetching user: " + ex.Message);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
        public async Task<List<UserTask>> GetTask(DateTime? date=null)
        {
            List<UserTask> tasks = new List<UserTask>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string filePath = "C:/Users/elangovan.devaraj/Downloads/Elangovan-Status Report.xlsx";

            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 4; row <= rowCount; row++)
                {
                    bool isEmptyRow = true;
                    for (int col = 1; col <= colCount; col++)
                    {
                        if (worksheet.Cells[row, col].Value != null)
                        {
                            isEmptyRow = false;
                            break;
                        }
                    }
                    if (!isEmptyRow)
                    {
                        UserTask data = new UserTask
                        {
                            Date = DateTime.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out DateTime taskDate) ? taskDate : DateTime.MinValue,
                            TaskDescription = worksheet.Cells[row, 2].Value?.ToString(),
                            HoursWorked = int.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out int hours) ? hours : 0,
                            Status = worksheet.Cells[row, 4].Value?.ToString()
                        };

                        if (!date.HasValue || data.Date.Date == date.Value.Date)
                        {
                            tasks.Add(data);
                        }
                    }
                }
            }

            return tasks;
        }
    }
}