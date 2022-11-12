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
using MallManagment.Api.Handlers;

namespace MallManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var employee = await _context.Adminstrators
                .Include(a=>a.Employee).FirstOrDefaultAsync(a=>a.EmployeeId==id);

            if (employee == null)
            {
                return Ok(new ResponseDto<AdminstratorDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<AdminstratorDto> {Model=employee, Status = ResponseStatus.Success });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var Adminstrators = await _context.Adminstrators
                    .Include(a=>a.Employee)
                 .Select(a => (AdminstratorDto)a).ToListAsync();
            return Ok(new ResponseDto<List<AdminstratorDto>> { Model = Adminstrators, Status = ResponseStatus.Success });
        }
       
        [HttpGet("pagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var Adminstrators =await  _context.Adminstrators
                .Search(pageParameter.SearchTerm)
                .Sort(pageParameter.OrderBy)
                  .Select(a => (AdminstratorDto)a).ToListAsync();

            var pagedLists= PagedList<AdminstratorDto>
                .ToPagedList(Adminstrators, pageParameter.PageNumber, pageParameter.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedLists.MetaData));

            return Ok(pagedLists);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]AdminstratorDto dto)
        {
            if (id != dto.EmployeeId)
            {
                 return Ok(new ResponseDto<AdminstratorDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            var current = DateTime.UtcNow;
            Adminstrator employee = dto;
            employee.ModifyDate = current;
            
            if (!string.IsNullOrEmpty(dto.Password))
            {
                PasswordHasher.GeneratePasswordHasing(dto.Password, out byte[] salt, out byte[] hash);
                employee.PasswordSalt = salt;
                employee.PasswordHash = hash;
            }

            _context.Entry(employee).State = EntityState.Modified;
           
            var userToken = await _context.UserTokens.Where(a => a.EmployeeId == dto.EmployeeId && a.TokenType == TokenType.EmailConfirmation).FirstOrDefaultAsync();
            if (userToken == null)
            {
                userToken = new UserToken
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = dto.EmployeeId,
                    TokenType = TokenType.EmailConfirmation,
                    Token = Guid.NewGuid().ToString(),
                    CreatedDate = current,
                    ModifyDate = current,
                    TokenExpireTime = current.AddDays(1)
                };
                _context.UserTokens.Add(userToken);
            }
            else
            {
                userToken.TokenExpireTime = current.AddDays(1);
                userToken.Token = Guid.NewGuid().ToString();
                _context.UserTokens.Update(userToken);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                {
                    return Ok(new ResponseDto<AdminstratorDto> { Model = employee, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<AdminstratorDto> { Model = employee, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<AdminstratorDto> { Model = employee, Status = ResponseStatus.Success});
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminstratorDto dto)
        {
            Adminstrator admin = dto;
            var current = DateTime.UtcNow;
            admin.CreatedDate =current;
            admin.ModifyDate =current;
            byte[] salt, hash;
            if (!string.IsNullOrEmpty(dto.Password))
                PasswordHasher.GeneratePasswordHasing(dto.Password, out salt, out hash);
            else
                PasswordHasher.GeneratePasswordHasing("Default@12", out salt, out hash);

            admin.PasswordSalt = salt;
            admin.PasswordHash = hash;
            _context.Adminstrators.Add(admin);

            var userToken = await _context.UserTokens.Where(a => a.EmployeeId == admin.EmployeeId && a.TokenType == TokenType.EmailConfirmation).FirstOrDefaultAsync();
            if (userToken == null)
            {
                userToken = new UserToken
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = dto.EmployeeId,
                    TokenType = TokenType.EmailConfirmation,
                    Token = Guid.NewGuid().ToString(),
                    CreatedDate = current,
                    ModifyDate = current,
                    TokenExpireTime = current.AddDays(1)
                };
                _context.UserTokens.Add(userToken);
            }
            else
            {
                userToken.TokenExpireTime=current.AddDays(1);
                userToken.Token = Guid.NewGuid().ToString();
                _context.UserTokens.Update(userToken);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(admin.EmployeeId))
                {
                    return Ok(new ResponseDto<AdminstratorDto> { Model = admin, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<AdminstratorDto> { Model = admin, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<AdminstratorDto> { Model = admin, Status = ResponseStatus.Success });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _context.Adminstrators.FindAsync(id);
            if (employee == null)
            {
                return Ok(new ResponseDto<AdminstratorDto> {Status = ResponseStatus.NotFound});
            }

            _context.Adminstrators.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<AdminstratorDto> {Model=employee, Status = ResponseStatus.Success});
        }

        private bool Exists(string id)
        {
            return _context.Adminstrators.Any(e => e.EmployeeId == id);
        }
    }
}
