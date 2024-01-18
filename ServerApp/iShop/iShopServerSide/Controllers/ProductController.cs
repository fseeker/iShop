using iShopServerSide.DAL;
using iShopServerSide.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace iShopServerSide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        ProductRepository ProductRepo;
        public ProductController(ProductRepository ProductRepo)
        {
            this.ProductRepo = ProductRepo;
        }
        [HttpGet("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {
            try
            {

                List<Product> products = ProductRepo.GetAllProducts();

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
        //[Authorize]
        //[HttpGet("cart")]
        //public Product GetCart(int es)
        //{
        //    //string userId = User.Identity.FindFirst();

        //    ClaimsIdentity identity = User.Identity as ClaimsIdentity;

        //    Claim idClaim = identity.FindFirst("Id");

        //    string userId = idClaim.Value;

        //    return null;
        //}

        //[Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("addproduct")]
        public int AddProduct(Product toAdd)
        {
            int id = 0;
            id = ProductRepo.AddProduct(toAdd);
            return id;
        }
        [Authorize]
        [HttpGet("getCart")]
        public IEnumerable<CartItem> GetCart()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;

            string userId = identity.FindFirst("Id").Value;

            if (string.IsNullOrEmpty(userId)) return new List<CartItem>();
            List<CartItem> CartItems = ProductRepo.GetCartItems(int.Parse(userId));
            return CartItems;
        }

        [Authorize]
        [HttpPost("addToCart")]
        public string AddToCart(Product product)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;

            string userId = identity.FindFirst("Id").Value;

            if (string.IsNullOrEmpty(userId)) return "userId not found";
            if (product == null) return "product not found";

            var success = ProductRepo.AddToCart(product.Id, int.Parse(userId), product.Quantity);

            return success.ToString();
        }

        [HttpPost("removeFromCart")]
        public bool RemoveFromCart(int ProductId, int UserId, int Quantity)
        {
            bool success = ProductRepo.RemoveFromCart(ProductId, UserId, Quantity);
            return success;
        }
    }
}