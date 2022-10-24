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
using MallManagment.Shared.Models.RequestFeatures;
using MallManagment.Server.Repositories.Extenssions;
using Newtonsoft.Json;

namespace MallManagment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return Ok(new ResponseDto<EmployeeDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<EmployeeDto> {Model=employee, Status = ResponseStatus.Success });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var employees = await _context.Employees
                 .Select(a => (EmployeeDto)a).ToListAsync();
            return Ok(new ResponseDto<List<EmployeeDto>> { Model = employees, Status = ResponseStatus.Success });
        }
       
        [HttpGet("pagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var employees =await  _context.Employees
                .Search(pageParameter.SearchTerm)
                .Sort(pageParameter.OrderBy)
                  .Select(a => (EmployeeDto)a).ToListAsync();

            var pagedLists= PagedList<EmployeeDto>
                .ToPagedList(employees, pageParameter.PageNumber, pageParameter.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedLists.MetaData));

            return Ok(pagedLists);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]EmployeeDto dto)
        {
            if (id != dto.Id)
            {
                 return Ok(new ResponseDto<EmployeeDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            Employee employee = dto;
            employee.ModifyDate = DateTime.UtcNow;
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                {
                    return Ok(new ResponseDto<EmployeeDto> { Model = employee, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<EmployeeDto> { Model = employee, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<EmployeeDto> { Model = employee, Status = ResponseStatus.Success});
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto dto)
        {
            var current = DateTime.UtcNow;
            dto.Id = Guid.NewGuid().ToString();
            dto.CreatedDate =current;
            dto.ModifyDate =current;
            _context.Employees.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(dto.Id))
                {
                    return Ok(new ResponseDto<EmployeeDto> { Model = dto, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<EmployeeDto> { Model = dto, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<EmployeeDto> { Model = dto, Status = ResponseStatus.Success });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return Ok(new ResponseDto<EmployeeDto> {Status = ResponseStatus.NotFound});
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<EmployeeDto> {Model=employee, Status = ResponseStatus.Success});
        }

        private bool Exists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
