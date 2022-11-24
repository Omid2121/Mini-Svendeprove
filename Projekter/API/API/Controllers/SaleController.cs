using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using VareskanningModels.DB;
using VareskanningModels.SQL;
using Microsoft.EntityFrameworkCore;
using VareskanningModels;
using RandomStringCreator;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ItemScannerDbContext _context;
        public SaleController(ItemScannerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used to show list of all the sold items.
        /// </summary>
        /// <returns></returns>
        // GET: api/<SaleController>
        [HttpGet]
        public IActionResult Get()
        {
            _ = _context.Products.ToList();
            _ = _context.Users.ToList();

            //var result = _context.Sales.ToList().Select(s => new
            //{
            //    s.Id,
            //    s.Timestamp,
            //    product = new
            //    {
            //        s.ProductId,
            //        s.Product.Name,
            //        s.Product.Price,
            //        s.Product.Barcode,
            //        s.Product.ProductType
            //    },
            //    user = new
            //    {
            //        s.UserId,
            //        s.User.Username
            //    }
            //});
            //return Ok(result);

            var saleDTO = _context.Sales.ToList().Select(s => new SaleDTO
            {
                Id = s.Id,
                Timestamp = s.Timestamp,

                ProductId = s.ProductId,
                Name = s.Product.Name,
                Price = s.Product.Price,
                Barcode = s.Product.Barcode,
                ProductType = s.Product.ProductType,

                UserId = s.UserId,
                Username = s.User.Username
            });
            return Ok(saleDTO);
        }

        ///// <summary>
        ///// This method is used to show the sold item by id.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //// GET api/<SaleController>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var sale = await _context.Sales.FindAsync(id);
        //    if (sale == null)
        //    {
        //        return NotFound($"Sold product with id {id} not found");
        //    }
        //    return Ok(sale);
        //}

        /// <summary>
        /// This method is used to add a new sold item.
        /// </summary>
        /// <param name="partialSale"></param>
        /// <returns></returns>
        // POST api/<SaleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PartialSale partialSale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? existingUser = _context.Users.FirstOrDefault(u => u.Id == partialSale.UserId);
            if (existingUser == null)
            {
                return NotFound($"User with id {partialSale.UserId} doesn't exist.");
            }

            Product? existingProduct = _context.Products.FirstOrDefault(p => p.Barcode == partialSale.Barcode);
            if (existingProduct == null)
            {
                return NotFound($"Scanned product with the barcode {partialSale.Barcode} doesn't exist.");
            }

            Sale sale = new()
            {
                Id = new StringCreator().Get(10),
                UserId = partialSale.UserId,
                ProductId = existingProduct.Id,
                Timestamp = DateTimeOffset.Now
            };
            
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return Ok($"Scanned product was added");
        }

        ///// <summary>
        ///// This method is used to update the sold item.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="sale"></param>
        ///// <returns></returns>
        //// PUT api/<SaleController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(string id, [FromBody] Sale sale)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    Sale? existingSale = _context.Sales.FirstOrDefault(s => s.Id == id);
        //    if (existingSale == null)
        //    {
        //        return NotFound($"Sold product with id {id} not found");
        //    }
        //    _context.Sales.Update(sale);
        //    await _context.SaveChangesAsync();

        //    var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:/api/sale/")
        //    {
        //        Content = new StringContent(JsonSerializer.Serialize(sale), Encoding.UTF8, "application/json")
        //    };
        //    await new HttpClient().SendAsync(request);

        //    return _context.Sales.FirstOrDefault(s => s.Id == id) != null
        //        ? Ok($"Sold product with id {id} was successfully updated")
        //        : BadRequest($"Sold product with id {id} was not updated");
        //}

        /// <summary>
        /// This method is used to delete the sold item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<SaleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound($"Sold product with id {id} not found");
            }
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return _context.Sales.FirstOrDefault(s => s.Id == sale.Id) == null
                ? Ok($"Sold product with id {sale.Id} was successfully deleted")
                : BadRequest($"Sold product with id {sale.Id} was not deleted");
        }

        /// <summary>
        /// This method is used to search the sold item by name.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        // GET: api/<SaleController>
        //[HttpGet]
        //[Route("search")]
        //public IActionResult Search(string searchValue)
        //{
        //    if (string.IsNullOrEmpty(searchValue))
        //    {
        //        return BadRequest("SearchValue must be provided.");
        //    }
        //    var result = _context.Sales.Where(s => s.Product.Name.Contains(searchValue.ToLower())).ToList();

        //    return Ok(result);
        //}
    }
}
