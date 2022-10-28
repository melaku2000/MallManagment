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
using MallManagment.Server.Handlers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MallManagment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var admin = await _context.Adminstrators.Include(a=>a.Employee).Where(a => a.Email == loginDto.Email).FirstOrDefaultAsync();
            if(admin == null)
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound,Message="Invalid username or password"});
            if (!admin.EmailConfirmed) return Ok(new ResponseDto<AuthDto> { Model = admin, Status = ResponseStatus.Unautorize, Message = "Email confirmation required" });

            var response = new ResponseDto<AuthDto>();
            try
            {
                var result = await PasswordHasher.VerifyPassword(loginDto.Password!, admin.PasswordSalt!, admin.PasswordHash!);
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
                AuthDto auth = admin;
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,auth.EmployeeId),
                    new Claim(ClaimTypes.Name,auth.FullName),
                    new Claim(ClaimTypes.Email,auth.Email),
                    new Claim(ClaimTypes.Role,auth.StringRole),
                };
                var claimIdentity = new ClaimsIdentity(claims,"serverAuth");
                var claimPrincipal=new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimPrincipal);
                response.Model = auth;
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured: {ex.Message}" });
            }
         
            return Ok(response);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpGet("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            AuthDto auth = new AuthDto();
            if (User.Identity.IsAuthenticated)
            {
                auth.EmployeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                auth.Email = User.FindFirstValue(ClaimTypes.Email);
                auth.FullName = User.FindFirstValue(ClaimTypes.Name);
                auth.StringRole = User.FindFirstValue(ClaimTypes.Role);
            }
            return Ok(auth);
        }

        [HttpPost("emailconfirm")]
        public async Task<IActionResult> EmailConfirmation([FromBody] ConfirmDto confirmDto)
        {
            var userToken=await _context.UserTokens.Where(a=>a.EmployeeId== confirmDto.EmployeeId && a.TokenType==TokenType.EmailConfirmation).FirstOrDefaultAsync();
            if(userToken==null)
                return Ok(new ResponseDto<ConfirmPasswordDto>() { Status=ResponseStatus.NotFound});

            if (!userToken.Token!.Equals(confirmDto.Token))
                return Ok(new ResponseDto<ConfirmPasswordDto>() {Model=userToken, Status = ResponseStatus.Unautorize, Message = "Invalid token" });

            var current = DateTime.UtcNow;
            if(userToken.TokenExpireTime<DateTime.UtcNow)
                return Ok(new ResponseDto<ConfirmPasswordDto>() { Model = userToken, Status = ResponseStatus.Error,Message=$"This token is expired {(DateTime.UtcNow.Subtract(userToken.TokenExpireTime)).Hours} hours ago" });

            var admin = await _context.Adminstrators.FirstOrDefaultAsync(a => a.EmployeeId == confirmDto.EmployeeId);
            if (admin != null)
            {
                admin.EmailConfirmed = true;
                admin.ModifyDate = current;
                _context.Adminstrators.Update(admin);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Ok(new ResponseDto<ConfirmPasswordDto>() { Status = ResponseStatus.Error,Message=ex.Message });
                }
            }
         
            return Ok(new ResponseDto<ConfirmPasswordDto>() { Model = userToken, Status = ResponseStatus.Success });
        }
        [HttpPost("setpassword")]
        public async Task<IActionResult> SetPassword([FromBody] ConfirmPasswordDto confirmDto)
        {
            var userToken = await _context.UserTokens.Where(a => a.EmployeeId == confirmDto.EmployeeId && a.TokenType == TokenType.EmailConfirmation).FirstOrDefaultAsync();
            if (userToken == null)
                return Ok(new ResponseDto<ConfirmPasswordDto>() { Status = ResponseStatus.NotFound });

            if (!userToken.Token!.Equals(confirmDto.Token))
                return Ok(new ResponseDto<ConfirmPasswordDto>() { Model = userToken, Status = ResponseStatus.Unautorize, Message = "Invalid token" });

            var current = DateTime.UtcNow;
            if (userToken.TokenExpireTime < DateTime.UtcNow)
                return Ok(new ResponseDto<ConfirmPasswordDto>() { Model = userToken, Status = ResponseStatus.Error, Message = $"This token is expired {(DateTime.UtcNow.Subtract(userToken.TokenExpireTime)).Hours} hours ago" });

            var admin = await _context.Adminstrators.FirstOrDefaultAsync(a => a.EmployeeId == confirmDto.EmployeeId);
            if (admin != null)
            {
                PasswordHasher.GeneratePasswordHasing(confirmDto.Password!, out byte[] salt, out byte[] hash);

                admin.PasswordSalt = salt;
                admin.PasswordHash = hash;
                admin.ModifyDate = current;
                _context.Adminstrators.Update(admin);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Ok(new ResponseDto<ConfirmPasswordDto>() { Status = ResponseStatus.Error, Message = ex.Message });
                }
            }

            return Ok(new ResponseDto<ConfirmPasswordDto>() { Model = userToken, Status = ResponseStatus.Success });
        }
    }
}
