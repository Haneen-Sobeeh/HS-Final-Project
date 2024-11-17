using Microsoft.AspNetCore.Mvc;
using Orange1.Models;

namespace Orange1.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly OrangeContext _context;

        public TestimonialController(OrangeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Testimonial()
        {
            var testimonials = _context.Tastimonials.ToList(); // Get all testimonials
            return View(testimonials);
        }

        [HttpPost]
        public IActionResult Testimonial([FromBody] string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                var userId = HttpContext.Session.GetString("IdUser");

                var testimonial = new Tastimonial
                {
                    CommentText = message,
                    CommenUser = userId ?? "Anonymous",
                    UserId = userId
                };

                _context.Tastimonials.Add(testimonial);
                _context.SaveChanges();

                return Json(new
                {
                    testimonial.Id,
                    testimonial.CommentText,
                    CommenUser = testimonial.CommenUser
                });
            }

            return BadRequest("Feedback cannot be empty.");
        }

        public IActionResult RemoveTestimonial(int id)
        {
            var testimonial = _context.Tastimonials.FirstOrDefault(t => t.Id == id);
            if (testimonial != null)
            {
                _context.Tastimonials.Remove(testimonial);
                _context.SaveChanges();
            }
            return RedirectToAction("Testimonial");
        }

        public IActionResult Dashboard()
        {
            var testimonials = _context.Tastimonials.ToList();
            return View(testimonials);
        }
    }
}
