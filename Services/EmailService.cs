using System.Net.Mail;
using System.Net;
using PoC_Demo.Model;
using PoC_Demo.Repository;
using System.Data.SqlClient;
using System.Data;

namespace PoC_Demo.Services
{
    public class EmailService: Connection
    {
        protected readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) : base(configuration)
        {
            _configuration=configuration;
        }
        public async Task SendMail(Product product,string emailSubject)
        {

            List<RecipientEmail> recipients =await Getrecipients();

            string password= _configuration["Email:PassKey"]??"";
            string ServerId= _configuration["Email:EmailId"] ?? "";
            string from = _configuration["Email:EmailId"] ?? "";
            string subject = emailSubject;
            string body = Emailbody(product);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(ServerId, password)
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            foreach (var email in recipients)
            {
                if (!string.IsNullOrEmpty(email?.Email))
                {
                    mailMessage.To.Add(email.Email);
                }
            }
            await client.SendMailAsync(mailMessage);
        }


        private string Emailbody(Product product)
        {
            string body = $@"
                        <html>
                        <body>
                            <p>Hello!

                    We're excited to announce that new products have been added to our lineup. This expansion enhances our offerings, providing more variety and value. Discover the latest additions today!</p>
                            <table border='1' style='margin: 0 auto;'>
                                <tr style='text-align: center;'>
                                    <th>Product Name</th>
                                    <th>Product Description</th>
                                    <th>Product Count</th>
                                </tr>
                                <tr style='text-align: center;'>                      
                                    <td>{product.ProductName}</td>
                                    <td>{product.ProductDescription}</td>
                                    <td>{product.ProductCount}</td>
                                </tr>
                            </table>
                        </body>
                        </html>";
            return body;
        }


        private async Task<List<RecipientEmail>> Getrecipients()
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("SPS_Getrecipients", SqlConnection))
                {
                    List<RecipientEmail> recipients = new List<RecipientEmail>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            recipients.Add(new RecipientEmail
                            {
                                Email = Convert.ToString(reader["Email"]),                   
                            });
                        }
                    }
                    return recipients;
                }
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
