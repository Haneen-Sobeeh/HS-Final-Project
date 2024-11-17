using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orange1.Models;
using System.Linq;

namespace Orange1.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly OrangeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderController(OrangeContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> ShopCart()
        {
            // Check if the user is authenticated
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // If user is not authenticated, redirect to the login page with a ReturnUrl to redirect back after login
                return RedirectToAction("Login", "Account", new { ReturnUrl = "/Order/ShopCart" });
            }

            // Fetch cart items for the authenticated user
            var cartItems = await _context.productInCards
                .Where(o => !o.IsConfirm && o.UserId == user.Id)
                .Include(o => o.Product) // Ensure Product navigation property is loaded
                .Select(o => new ProductInCartViewModel
                {
                    OrderProductId = o.Id,
                    ProductName = o.Product.ProductName,
                    ImagePath = o.Product.ImagePath,
                    Price = o.Product.Price,
                    Quantity = o.Quantity+"",
                    Date=o.Date,
					ProductColor = o.ProductColor.ToString(), // Convert enum to string
					ProductSize = o.ProductSize.ToString() // Convert enum to string
				})
                .ToListAsync();

            var viewModel = new ProductsViewModel
            {
                CartItems = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AjaxProductViewModel model)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // If user is not authenticated, return a JSON response to indicate the need for redirection
                return Json(new { redirectToLogin = true, loginUrl = Url.Action("Login", "Account", new { ReturnUrl = $"/Product/Details/{model.ProductId}" }) });
            }

            // Map the data from AjaxProductViewModel to ProductInCard
            var productInCard = new ProductInCard
            {
                UserId = user.Id,
                Date = DateOnly.FromDateTime(DateTime.Now),
                ProductId = int.TryParse(model.ProductId, out var productId) ? productId : 0,
                Quantity =Int32.Parse(model.Quantity), // Quantity as a string
                ProductColor = Enum.TryParse<Color>(model.ProductColor, out var color) ? color : Color.Black, // Default to Black if parsing fails
                ProductSize = Enum.TryParse<Size>(model.ProductSize, out var size) ? size : Size.S // Default to Size S if parsing fails
            };

            // Add the object to the context and save changes
            _context.productInCards.Add(productInCard);
            await _context.SaveChangesAsync();

            // Return a JSON result to indicate success
            return Json(new { success = true, message = "Product added to cart!" });
        }
         
        public IActionResult Remove(int id)
        {
            ProductInCard productIn= _context.productInCards.FirstOrDefault(x => x.Id == id);
            if (productIn != null)
            {
                _context.Remove(productIn);
                _context.SaveChanges();
                return RedirectToAction("ShopCart");
            }
            return RedirectToAction("NotFound","Home");
        }
    }
}