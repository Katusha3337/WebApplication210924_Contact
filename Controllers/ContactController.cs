using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WebApplication210924_Contact.Models;

namespace WebApplication210924_Contact.Controllers
{
    //https://localhost:7097/Contact
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                var smtpClient = new SmtpClient("smtp.example.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(model.Email),
                    Subject = model.Subject,
                    Body = model.Message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add("admin@example.com");

                try
                {
                    smtpClient.Send(mailMessage);
                    return RedirectToAction("Success");
                }
                catch (SmtpException ex)
                {
                    // Обработка ошибок отправки почты
                    ModelState.AddModelError("", "Ошибка отправки почты: " + ex.Message);
                }
            }

            return View("Index", model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
