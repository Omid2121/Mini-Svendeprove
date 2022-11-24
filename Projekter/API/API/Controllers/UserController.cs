using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using VareskanningModels;
using VareskanningModels.DB;
using VareskanningModels.SQL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ItemScannerDbContext _context;
        public UserController(ItemScannerDbContext context)
        {
            _context = context;
        }

        ///// <summary>
        ///// This method is used for login process.
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("login")]
        //public IActionResult Login(string username, string password)
        //{
        //    var existingUser = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        //    if (existingUser == null)
        //    {
        //        return NotFound($"User with username {username} not found");
        //    }
        //    return Ok(existingUser);
        //}

        /// <summary>
        /// This method is used for login process.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (existingUser == null)
            {
                return NotFound($"User with username {login.Username} not found");
            }
            return Ok(existingUser);
        }


        /// <summary>
        /// This method is used to show list of all the users.
        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        /// <summary>
        /// This method is used to show the user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }
            return Ok(user);
        }

        /// <summary>
        /// This method is used to add a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //User? savedUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            //if (savedUser != null)
            //{
            //    return BadRequest($"User with username {user.Username} already exists");
            //}

            User? savedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (savedUser != null)
            {
                return BadRequest($"User with id {user.Id} already exists");
            }


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _context.Users.FirstOrDefault(u => u.Username == user.Username) != null
                ? Ok($"User with username {user.Username} was successfully added")
                : BadRequest($"User with username {user.Username} was not added");
        }

        /// <summary>
        /// This method is used to update a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound($"User with id {id} not found");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:/api/user/")
            {
                Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json")
            };
            await new HttpClient().SendAsync(request);

            return _context.Users.FirstOrDefault(u => u.Id == id) != null
                ? Ok($"User with id {id} was successfully updated")
                : BadRequest($"User with id {id} was not updated");
        }

        /// <summary>
        /// This method is used to delete a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return _context.Users.FirstOrDefault(u => u.Username == user.Username) == null
                ? Ok($"User with username {user.Username} was successfully deleted")
                : BadRequest($"User with username {user.Username} was not deleted");
        }
    }
}
