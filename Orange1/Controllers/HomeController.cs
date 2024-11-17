using Microsoft.AspNetCore.Mvc;
using Orange1.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Orange1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly OrangeContext db;
		public HomeController(ILogger<HomeController> logger, OrangeContext db)
		{
			_logger = logger;
			this.db = db;
		}

		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Contact(string name, string email, string subject, string message)
		{
			if (ModelState.IsValid)
			{
				// SMTP Client configuration (update with your SMTP settings)
				var smtpClient = new SmtpClient("smtp.your-email-provider.com")
				{
					Port = 587, // Adjust port if necessary
					Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
					EnableSsl = true
				};

				// Compose the email
				var mailMessage = new MailMessage
				{
					From = new MailAddress("your-email@example.com"),
					Subject = subject,
					Body = $"<p><strong>Name:</strong> {name}</p>" +
						   $"<p><strong>Email:</strong> {email}</p>" +
						   $"<p><strong>Message:</strong></p><p>{message}</p>",
					IsBodyHtml = true
				};

				// Send to target email
				mailMessage.To.Add("alkurdihom@gmail.com");

				try
				{
					await smtpClient.SendMailAsync(mailMessage);
					ViewBag.Message = "Your message has been sent successfully!";
				}
				catch
				{
					ViewBag.Message = "There was an error sending your message. Please try again later.";
				}
			}

			return View();
		}
	
	public IActionResult Privacy()
		{
			return View();
		}
		// Action to display the session data
		public IActionResult Index()
		{
			// Initialize the HomeViewModel with data from Products and Categories tables
			HomeViewModel viewModel = new HomeViewModel
			{
				Products = db.Products.ToList(),
				Categories = db.Categories.ToList(),
                Testimonials = db.Tastimonials.ToList(),
             	
            };

			return View(viewModel);
		}

		public IActionResult Shop()
		{
			// Initialize the HomeViewModel with data from Products and Categories tables
			HomeViewModel viewModel = new HomeViewModel
			{
				Products = db.Products.ToList(),
				Categories = db.Categories.ToList()
			};

			return View(viewModel);
		}

		public IActionResult NotFound()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}




    }



}
