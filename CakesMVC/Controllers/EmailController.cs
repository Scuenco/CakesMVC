using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail; 
using SendGrid;
using CakesMVC.Model;
using System.Net;
using System.Threading.Tasks; 

namespace CakesMVC.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Email em)
        {
            // create an email 
            SendGridMessage mail = new SendGridMessage();
            mail.From = new MailAddress("info.confections@gmail.com");
            mail.AddTo(em.Subscriber);
            mail.Subject = "A Message From Confections";
            mail.Html = "<p>Thank you for joining our mailing list.</p>";
            mail.Text = "Thank you for joining our mailing list.";
           
            // create network credentials
            var username = System.Environment.GetEnvironmentVariable("sendgrid_user");
            var password = System.Environment.GetEnvironmentVariable("sendgrid_pw");
            var credentials = new NetworkCredential(username, password);
            
            // create a Web transport for sending email
            var transportWeb = new Web(credentials);

            // send the email
            transportWeb.DeliverAsync(mail);
            ViewBag.Message = "Thank you for joining our mailing list.";
            return View("Index");
        }
    }
}