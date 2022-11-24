using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareskanningModels;
using VareskanningModels.DB;
using VareskanningModels.SQL;

namespace API.Controllers
{
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : HasId
    {
        protected readonly ItemScannerDbContext _context;
        protected DbSet<TEntity> _dbSet { get; set; }
        
        public BaseController(ItemScannerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        ///// <summary>
        ///// This generic method is used to show list of _dbSet.
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public virtual IActionResult Get()
        //{
        //    return Ok( _dbSet.ToListAsync());
        //}

        ///// <summary>
        ///// This generic method is used to show _dbSet by Id.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //public virtual async Task<IActionResult> Get(string id)
        //{
            
        //}

        ///// <summary>
        ///// This generic method is used to create new entity in _dbSet. 
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public virtual async Task<IActionResult> Post([FromBody] TEntity entity) 
        //{
            
        //}

        ///// <summary>
        ///// This generic method is used to update existing entity in _dbSet.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public virtual async Task<IActionResult> Put(string id, [FromBody] TEntity entity)
        //{
            
        //}

        ///// <summary>
        ///// This generic method is used to delete existing entity in _dbSet.
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public virtual async Task<IActionResult> Delete(string id) 
        //{
            
        //}
    }
}
