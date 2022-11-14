using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MallManagment.Api.Data;
using MallManagment.Models.Entities;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;

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
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var catagories = await _context.BusinessCatagories
                 .Select(a => (CatagoryDto)a).ToListAsync();
            return Ok(new ResponseDto<List<CatagoryDto>> { Model = catagories, Status = ResponseStatus.Success });
        }

        // GET: api/Catagories/5
        // GET: api/Catagories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessCatagory(string id)
        {
            var catagory = await _context.BusinessCatagories.FindAsync(id);

            if (catagory == null)
            {
                return Ok(new ResponseDto<CatagoryDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<CatagoryDto> { Model = catagory, Status = ResponseStatus.Success });
        }

        // PUT: api/Catagories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessCatagory(string id, [FromBody] CatagoryDto businessCatagory)
        {
            if (id != businessCatagory.Id)
            {
                return BadRequest();
            }
            BusinessCatagory catagory = businessCatagory;
            catagory.ModifyDate = DateTime.UtcNow;
            _context.Entry(catagory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BusinessCatagoryExists(id))
                {
                    return Ok(new ResponseDto<CatagoryDto> { Model = catagory, Status = ResponseStatus.NotFound });
                }
                else
                {
                    return Ok(new ResponseDto<CatagoryDto> { Model = catagory, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }

            return Ok(new ResponseDto<CatagoryDto> { Model = catagory, Status = ResponseStatus.Success });
        }

        // POST: api/Catagories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CatagoryDto dto)
        {
            var current = DateTime.UtcNow;
            dto.Id = Guid.NewGuid().ToString();
            dto.CreatedDate = current;
            dto.ModifyDate = current;
            _context.BusinessCatagories.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (BusinessCatagoryExists(dto.Id))
                {
                    return Ok(new ResponseDto<CatagoryDto> { Model = dto, Status = ResponseStatus.Error, Message = "Its already exists" });
                }
                else
                {
                    return Ok(new ResponseDto<CatagoryDto> { Model = dto, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }

            return Ok(new ResponseDto<CatagoryDto> { Model = dto, Status = ResponseStatus.Success });
        }
        // DELETE: api/Catagories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var catagory = await _context.BusinessCatagories.FindAsync(id);
            if (catagory == null)
            {
                return Ok(new ResponseDto<CatagoryDto> { Status = ResponseStatus.NotFound });
            }

            _context.BusinessCatagories.Remove(catagory);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<CatagoryDto> { Model = catagory, Status = ResponseStatus.Success });
        }

        private bool BusinessCatagoryExists(string id)
        {
            return _context.BusinessCatagories.Any(e => e.Id == id);
        }
    }
}
