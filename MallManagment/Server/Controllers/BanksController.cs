using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MallManagment.Server.Data;
using MallManagment.Shared.Models;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using System.Security.Cryptography;

namespace MallManagment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BanksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            return await _context.Banks.ToListAsync();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBank(string id)
        {
            var bank = await _context.Banks.FindAsync(id);

            if (bank == null)
            {
                return Ok(new ResponseDto<BankDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<BankDto> {Model=bank, Status = ResponseStatus.Success });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBankList()
        {
            var banks = await _context.Banks
                 .Select(a => (BankDto)a).ToListAsync();
            return Ok(new ResponseDto<List<BankDto>> { Model = banks, Status = ResponseStatus.Success });
        }

        // PUT: api/Banks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(string id, [FromBody]BankDto dto)
        {
            if (id != dto.Id)
            {
                 return Ok(new ResponseDto<BankDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            Bank bank = dto;
            bank.ModifyDate = DateTime.UtcNow;
            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BankExists(id))
                {
                    return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Success});
        }

        // POST: api/Banks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBank([FromBody]BankDto bank)
        {
            var current = DateTime.UtcNow;
            bank.Id = Guid.NewGuid().ToString();
            bank.CreatedDate =current;
            bank.ModifyDate =current;
            _context.Banks.Add(bank);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (BankExists(bank.Id))
                {
                    return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Success });
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(string id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return Ok(new ResponseDto<BankDto> {Status = ResponseStatus.NotFound});
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<BankDto> {Model=bank, Status = ResponseStatus.Success});
        }

        private bool BankExists(string id)
        {
            return _context.Banks.Any(e => e.Id == id);
        }
    }
}
