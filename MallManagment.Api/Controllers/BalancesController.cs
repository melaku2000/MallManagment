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
    public class BalancesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BalancesController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var reading = await _context.Balances.FindAsync(id);

            if (reading == null)
            {
                return Ok(new ResponseDto<BalanceDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<BalanceDto> {Model= reading, Status = ResponseStatus.Success });
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] BalanceRead balance)
        {
            DateTime currentTime=balance.CreatedDate;
            List<BalanceDto> balances = new List<BalanceDto>();
            balances = await _context.Balances
                .Where(a => a.CreatedDate.Date== balance.CreatedDate.Date)
                .Include(a => a.Bank)
                .Include(a => a.Employee)
                 .Select(a => (BalanceDto)a).ToListAsync();

            return Ok(new ResponseDto<List<BalanceDto>> { Model = balances, Status = ResponseStatus.Success,Message=$"Date is {currentTime}" });
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]BalanceDto dto)
        {
            if (id != dto.Id)
            {
                 return Ok(new ResponseDto<BalanceDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            Balance reading = dto;
            reading.ModifyDate = DateTime.UtcNow;
            _context.Entry(reading).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                {
                    return Ok(new ResponseDto<BalanceDto> { Model = reading, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<BalanceDto> { Model = reading, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<BalanceDto> { Model = reading, Status = ResponseStatus.Success});
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BalanceDto dto)
        {
            var current = DateTime.UtcNow;
            dto.Id = Guid.NewGuid().ToString();
            dto.CreatedDate =current;
            dto.ModifyDate =current;
            _context.Balances.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(dto.Id))
                {
                    return Ok(new ResponseDto<BalanceDto> { Model = dto, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<BalanceDto> { Model = dto, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<BalanceDto> { Model = dto, Status = ResponseStatus.Success });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reading= await _context.Balances.FindAsync(id);
            if (reading == null)
            {
                return Ok(new ResponseDto<BalanceDto> {Status = ResponseStatus.NotFound});
            }

            _context.Balances.Remove(reading);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<BalanceDto> {Model= reading, Status = ResponseStatus.Success});
        }

        private bool Exists(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
