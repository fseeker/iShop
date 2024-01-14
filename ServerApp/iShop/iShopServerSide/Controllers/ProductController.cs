using iShopServerSide.DAL;
using iShopServerSide.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public IEnumerable<Product> GetProduct()
        {
            string filePath = "productsJson.json";

            try
            {
                //string jsonString = System.IO.File.ReadAllText(filePath);

                List<Product> robotParts = ProductRepo.GetAllProducts();
                //JsonSerializer.Deserialize<List<Product>>(jsonString);

                return robotParts;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
        [HttpGet("cart")]
        public Product GetCart()
        {
            string filePath = "productsJson.json";

            try
            {
                string jsonString = System.IO.File.ReadAllText(filePath);

                List<Product> robotParts = JsonSerializer.Deserialize<List<Product>>(jsonString);

                return robotParts.First();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }

        }
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
        public IEnumerable<CartItem> GetCart(int UserId)
        {
            string? user = User.Identity.Name;
            List<CartItem> CartItems = ProductRepo.GetCartItems(UserId);
            return CartItems;
        }

        [HttpPost("addToCart")]
        public async Task<bool> AddToCart()
        {
            string requestBody;

            using (var reader = new StreamReader(Request.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            dynamic cartData = JObject.Parse(requestBody);

            int productId = cartData.productId;
            int userId = cartData.userId;
            int quantity = cartData.quantity;

            bool success = ProductRepo.AddToCart(productId, userId, quantity);
            return success;
        }

        [HttpPost("removeFromCart")]
        public bool RemoveFromCart(int ProductId, int UserId, int Quantity)
        {
            bool success = ProductRepo.RemoveFromCart(ProductId, UserId, Quantity);
            return success;
        }
    }
}