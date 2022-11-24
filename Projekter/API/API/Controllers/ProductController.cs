using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using VareskanningModels.DB;
using VareskanningModels.SQL;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ItemScannerDbContext _context;

        public ProductController(ItemScannerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used to show list of all the products.
        /// </summary>
        /// <returns></returns>
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Products.ToList());
        }

        /// <summary>
        /// This method is used to show the product by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }
            return Ok(product);
        }

        /// <summary>
        /// This method is used to add a new product.
        /// </summary>
        /// <param name="value"></param>
        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Product? existingProduct = _context.Products.FirstOrDefault(p => p.Name.ToLower() == product.Name.ToLower() || p.Barcode == product.Barcode);
            //if (existingProduct != null)
            //{
            //    return BadRequest($"Product with description {product} already exists");
            //}

            Product? existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                return BadRequest($"Product with id {product} already exists");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _context.Products.FirstOrDefault(p => p.Name == product.Name) != null
                ? Ok($"Product with nameof {product.Name} was successfully added")
                : BadRequest($"Product with nameof {product.Name} was not added");
        }

        /// <summary>
        /// This method is used to update product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product? existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound($"Product with id {id} not found");
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:/api/product/")
            {
                Content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
            };
            await new HttpClient().SendAsync(request);

            return _context.Products.FirstOrDefault(p => p.Id == id) != null
                ? Ok($"Product with id {id} was successfully updated")
                : BadRequest($"Product with id {id} was not updated");
        }

        /// <summary>
        /// This method is used to delete product.
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var prodcut = await _context.Products.FindAsync(id);
            if (prodcut == null)
            {
                return NotFound($"Product with id {id} not found.");
            }
            _context.Products.Remove(prodcut);
            await _context.SaveChangesAsync();

            return _context.Products.FirstOrDefault(p => p.Name == prodcut.Name) == null
                ? Ok($"Product with name {prodcut.Name} was successfully deleted")
                : BadRequest($"Prodcut with name {prodcut.Name} was not deleted");
        }

        ///// <summary>
        ///// This method is used to search the product by name.
        ///// </summary>
        ///// <param name="searchValue"></param>
        ///// <returns></returns>
        //// GET: api/<ProductController>
        //[HttpGet]
        //[Route("search")]
        //public IActionResult Search(string searchValue)
        //{
        //    if (string.IsNullOrEmpty(searchValue))
        //    {
        //        return BadRequest("SearchValue must be provided.");
        //    }
        //    var result = _context.Products.Where(s => s.Name.Contains(searchValue.ToLower())).ToList();

        //    return Ok(result);
        //}
    }
}
