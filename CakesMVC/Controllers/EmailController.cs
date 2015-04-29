using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail; //for MailMessage, SmtpClient
using System.Windows.Forms;
using CakesMVC.Model; //for MessageBox

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
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("info.confections@gmail.com");
                mail.From = new MailAddress(em.From);
                mail.Subject = em.Subject;
                mail.Body = em.Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("info.confections", "CoderCamps0315");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("Your email was sent.");
                //return View("Index", em);
                return View();
            }
            else
            {
                MessageBox.Show("An error occurred while sending your email request.");
                return View();
            }
        }
    }
}