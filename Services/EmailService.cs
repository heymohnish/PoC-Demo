using System.Net.Mail;
using System.Net;
using PoC_Demo.Model;

namespace PoC_Demo.Services
{
    public class EmailService
    {
        public Task SendMail(Product product,string emailSubject)
        {

            string to = "elangomano17@gmail.com";
            string from = "elangomano17@gmail.com";
            string subject = emailSubject;
            string body = $@"
                <html>
                <body>
                    <p>Hello!

We're excited to announce that new products have been added to our lineup. This expansion enhances our offerings, providing more variety and value. Discover the latest additions today!</p>
                    <table border='1'>
                        <tr>
                            <th>Product Name</th>
                            <th>Product Description</th>
                            <th>Product Count</th>
                        </tr>
                        <tr>                      
                            <td>{product.ProductName}</td>
                            <td>{product.ProductDescription}</td>
                            <td>{product.ProductCount}</td>
                        </tr>
                    </table>
                </body>
                </html>";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("elangomano17@gmail.com", "uefg kudi dumf qttk")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            /*foreach (var email in recipientEmails)
            {
            }*/
                mailMessage.To.Add("email");

            return client.SendMailAsync(mailMessage);
        }
    }
}
