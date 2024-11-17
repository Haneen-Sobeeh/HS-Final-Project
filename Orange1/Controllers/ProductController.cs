using Orange1.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Orange1.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly OrangeContext db;

        public ProductController(OrangeContext context)
        {
            db = context;
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        [AllowAnonymous]
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {

            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            // Prepare any data needed for the create view
            return View();
        }
        [AllowAnonymous]
        public ActionResult Menu()
        {
            var products = db.Products.ToList();

            // Create a new HomeViewModel and populate it with products
            var viewModel = new HomeViewModel
            {
                Products = products
            };

            return View(viewModel);
        }


        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product, IFormFile ImageFile)
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
                        return View(product);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    product.ImagePath = "/images/" + uniqueFileName;
                }

                // Get the current user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                product.UserId = userId;



                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product, IFormFile ImageFile)
        {
            var existingProduct = db.Products.Find(id);
            if (existingProduct == null)
            {
                return RedirectToAction("NotFound", "Home"); // Handle if product not found
            }

            if (ModelState.IsValid)
            {
                // Update product properties
                existingProduct.ProductName = product.ProductName;
                existingProduct.Condition = product.Condition;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

                // Handle image upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(ImageFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageFile", "Invalid image format. Only JPG, JPEG, PNG, and GIF are allowed.");
                        return View(product);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    // Optionally, delete the old image file here if it exists
                    if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    existingProduct.ImagePath = "/images/" + uniqueFileName;
                }

                db.Products.Update(existingProduct);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(product);
        }
        [AllowAnonymous]

        public JsonResult GetCategories()
        {
            List<Categories> categories = db.Categories.ToList();
            return Json(categories);
        }
        [AllowAnonymous]
        // Remaining actions...
        // Action for proceeding to checkout
        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = db.productInCards
                .Where(p => p.UserId == userId && !p.IsConfirm)
                .Select(p => new ProductInCartViewModel
                {
                    OrderProductId = p.Id,
                    ProductName = p.Product.ProductName,
                    ImagePath = p.Product.ImagePath,
                    Price = p.Product.Price,
                    Quantity = p.Quantity.ToString(),
                    ProductColor = p.ProductColor.ToString(),                  
                    Date = p.Date
                }).ToList();

            var orderViewModel = new OrderViewModel
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(item => item.TotalPrice)
            };

            return View(orderViewModel);
        }
        [AllowAnonymous]
        public IActionResult ConfirmOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Eagerly load related Product data with Include
            var cartItems = db.productInCards
                .Include(p => p.Product) // Eagerly load Product data
                .Where(p => p.UserId == userId && !p.IsConfirm)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("ShopCart");
            }

            var newOrder = new Order
            {
                UserId = userId,
                // Calculate total price, checking for null Product
                TotalPrice = cartItems
                    .Where(item => item.Product != null) // Filter out any null Products
                    .Sum(item => item.Quantity * item.Product.Price),

                // Create OrderItems, ensuring we only include items with valid Products
                OrderItems = cartItems
                    .Where(item => item.Product != null) // Filter out null Products
                    .Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        ProductColor = item.ProductColor,
                        ProductSize = item.ProductSize
                    }).ToList()
            };

            db.Orders.Add(newOrder);

            // Mark items as confirmed only if they have a valid Product
            foreach (var item in cartItems.Where(item => item.Product != null))
            {
                item.IsConfirm = true;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Home", new { orderId = newOrder.OrderId });

        }
        [AllowAnonymous]
        // Optional: Order confirmation page
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = db.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == orderId);

            return order == null ? NotFound() : View(order);
        }
    }
}