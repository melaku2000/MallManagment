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
using System.Security.Cryptography;
using MallManagment.Models.RequestFeatures;
using MallManagment.Api.Repositories.Extenssions;
using Newtonsoft.Json;

namespace MallManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return Ok(new ResponseDto<CustomerDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<CustomerDto> {Model= customer, Status = ResponseStatus.Success });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var customers = await _context.Customers
                .Include(a=>a.Catagory)
                 .Select(a => (CustomerDto)a).ToListAsync();
            return Ok(new ResponseDto<List<CustomerDto>> { Model = customers, Status = ResponseStatus.Success });
        }
       
        [HttpGet("pagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var customers =await  _context.Customers
                .Search(pageParameter.SearchTerm)
                .Sort(pageParameter.OrderBy)
                  .Select(a => (CustomerDto)a).ToListAsync();

            var pagedLists= PagedList<CustomerDto>
                .ToPagedList(customers, pageParameter.PageNumber, pageParameter.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedLists.MetaData));

            return Ok(pagedLists);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]CustomerDto dto)
        {
            if (id != dto.Id)
            {
                 return Ok(new ResponseDto<CustomerDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            Customer customer = dto;
            customer.ModifyDate = DateTime.UtcNow;
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                {
                    return Ok(new ResponseDto<CustomerDto> { Model = customer, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<CustomerDto> { Model = customer, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<CustomerDto> { Model = customer, Status = ResponseStatus.Success});
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto dto)
        {
            var current = DateTime.UtcNow;
            dto.Id = Guid.NewGuid().ToString();
            dto.CreatedDate =current;
            dto.ModifyDate =current;
            _context.Customers.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(dto.Id))
                {
                    return Ok(new ResponseDto<CustomerDto> { Model = dto, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<CustomerDto> { Model = dto, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<CustomerDto> { Model = dto, Status = ResponseStatus.Success });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return Ok(new ResponseDto<CustomerDto> {Status = ResponseStatus.NotFound});
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<CustomerDto> {Model=customer, Status = ResponseStatus.Success});
        }

        private bool Exists(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
