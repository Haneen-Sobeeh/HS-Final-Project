using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange1.Models;
using System.Threading.Tasks;

namespace Orange1.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        private readonly OrangeContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(OrangeContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("SignIn", "Account");
            }

            // Create a UserViewModel and populate it with user details and products
            UserViewModel userView = new UserViewModel
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                products = await db.Products
                                   .Include(product => product.Category) // Load full Category object
                                   .Where(product => product.UserId == user.Id)
                                   .ToListAsync(),
                NumberOfProducts = await db.Products.CountAsync(product => product.UserId == user.Id),
                TotalPrice = await db.Products.Where(product => product.UserId == user.Id)
                                              .SumAsync(product => product.Price)
            };

            // Pass data to the view
            return View(userView);
        }
        // GET: Dashboard/CreateCategory
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        // POST: Dashboard/CreateCategory
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Categories category, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Save the image file
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(ImageFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageFile", "Invalid image format. Only JPG, JPEG, PNG, and GIF are allowed.");
                        return View(category);
                    }

                    // Generate a unique filename and save the image
                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Set the image path for the category
                    category.ImagePath = "/images/" + uniqueFileName;
                }

                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(category);
        }

      


       
    }
}
