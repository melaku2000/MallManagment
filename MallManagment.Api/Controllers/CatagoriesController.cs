using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MallManagment.Api.Data;
using MallManagment.Models.Entities;

namespace MallManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CatagoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Catagories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessCatagory>>> GetBusinessCatagories()
        {
            return await _context.BusinessCatagories.ToListAsync();
        }

        // GET: api/Catagories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessCatagory>> GetBusinessCatagory(string id)
        {
            var businessCatagory = await _context.BusinessCatagories.FindAsync(id);

            if (businessCatagory == null)
            {
                return NotFound();
            }

            return businessCatagory;
        }

        // PUT: api/Catagories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessCatagory(string id, BusinessCatagory businessCatagory)
        {
            if (id != businessCatagory.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessCatagory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessCatagoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Catagories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BusinessCatagory>> PostBusinessCatagory(BusinessCatagory businessCatagory)
        {
            var current = DateTime.UtcNow;
            businessCatagory.CreatedDate = current;
            businessCatagory.ModifyDate = current;

            _context.BusinessCatagories.Add(businessCatagory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BusinessCatagoryExists(businessCatagory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBusinessCatagory", new { id = businessCatagory.Id }, businessCatagory);
        }

        // DELETE: api/Catagories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessCatagory(string id)
        {
            var businessCatagory = await _context.BusinessCatagories.FindAsync(id);
            if (businessCatagory == null)
            {
                return NotFound();
            }

            _context.BusinessCatagories.Remove(businessCatagory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessCatagoryExists(string id)
        {
            return _context.BusinessCatagories.Any(e => e.Id == id);
        }
    }
}
