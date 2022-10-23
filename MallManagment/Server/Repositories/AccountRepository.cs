using MallManagment.Server.Data;
using MallManagment.Server.Handlers;
using MallManagment.Server.Interfaces;
using MallManagment.Shared.Dtos;
using MallManagment.Shared.Enums;
using MallManagment.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Server.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AppDbContext _context { get; }
        private readonly Random _random = new Random();
        public AccountRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<AuthDto>> VerifyUser(LoginDto dto)
        {
            Adminstrator? admin = await _context.Adminstrators
                .FirstOrDefaultAsync(a => a.Email == dto.Email!);

            if (admin == null) return new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Invalid login attempt." };

            if (!admin.EmailConfirmed) return new ResponseDto<AuthDto> { Model = admin, Status = ResponseStatus.Unautorize, Message = "Email confirmation required" };
          
            var response = new ResponseDto<AuthDto>();

            try
            {
                var result = await PasswordHasher.VerifyPassword(dto.Password!, admin.PasswordSalt!, admin.PasswordHash!);
                if (result)
                {
                    admin.LastLoginTime = DateTime.UtcNow;
                    response.Status = ResponseStatus.Success;
                }
                else
                {
                    admin.LockCount += 1;
                    response.Status = ResponseStatus.Error;
                    response.Message = "Invalid user name or password.";
                }

                _context.Adminstrators.Update(admin);
                await _context.SaveChangesAsync();
                response.Model = admin;
            }
            catch (Exception ex)
            {
                return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured: {ex.Message}" };
            }
            return response;
        }
    }
}
