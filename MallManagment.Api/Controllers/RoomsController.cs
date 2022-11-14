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
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return Ok(new ResponseDto<RoomDto> { Status = ResponseStatus.NotFound });
            }

            return Ok(new ResponseDto<RoomDto> {Model= room, Status = ResponseStatus.Success });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            List<RoomDto> rooms= await _context.Rooms
                 .Select(a => (RoomDto)a).ToListAsync();

            return Ok(new ResponseDto<List<RoomDto>> { Model = rooms, Status = ResponseStatus.Success });
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]RoomDto dto)
        {
            if (id != dto.Id)
            {
                 return Ok(new ResponseDto<RoomDto> { Model = dto, Status = ResponseStatus.Unautorize});
            }
            Room room = dto;
            room.ModifyDate = DateTime.UtcNow;
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Exists(id))
                {
                    return Ok(new ResponseDto<RoomDto> { Model = room, Status = ResponseStatus.NotFound});
                }
                else
                {
                    return Ok(new ResponseDto<RoomDto> { Model = room, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<RoomDto> { Model = room, Status = ResponseStatus.Success});
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomDto dto)
        {
            var current = DateTime.UtcNow;
            dto.Id = Guid.NewGuid().ToString();
            dto.CreatedDate =current;
            dto.ModifyDate =current;
            _context.Rooms.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (Exists(dto.Id))
                {
                    return Ok(new ResponseDto<RoomDto> { Model = dto, Status = ResponseStatus.Error ,Message="Its already exists"});
                }
                else
                {
                    return Ok(new ResponseDto<RoomDto> { Model = dto, Status = ResponseStatus.Error ,Message=ex.Message});
                }
            }

            return Ok(new ResponseDto<RoomDto> { Model = dto, Status = ResponseStatus.Success });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var room= await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return Ok(new ResponseDto<RoomDto> {Status = ResponseStatus.NotFound});
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<RoomDto> {Model= room, Status = ResponseStatus.Success});
        }

        private bool Exists(string id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
