using MyVendors.BAL;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class EmailAPIController : ApiController
    {
        [System.Web.Http.HttpPost]
        public int SendMail([FromBody]Registration data)
        {
            string to = "avitailor1214@gmail.com";
            string subject = "Successfully Registered";


            string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + "EmailTemplate/Registration_Email.html";

            StreamReader str = new StreamReader(FilePath);
            string body = str.ReadToEnd();
            str.Close();
            body = body.Replace("#[{UserName}]#", data.Email);
            body = body.Replace("#[{FirstName}]#", data.Fname);
            body = body.Replace("#[{url}]#", "http://localhost:55006/Registration/Login");

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("demodemo471@gmail.com");
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("demodemo471@gmail.com", "Demo@12345");
            smtp.Send(mm);
            return 1;
        }
    }
}
