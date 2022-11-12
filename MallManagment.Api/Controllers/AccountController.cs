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
using MallManagment.Api.Handlers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MallManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenManager _tokenManager;

        public AccountController(AppDbContext context, ITokenManager tokenManager)
        {
            _context = context;
            _tokenManager = tokenManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var tokenExpireTimeHour = 6;

            var admin = await _context.Adminstrators.Include(a=>a.Employee).Where(a => a.Email == loginDto.Email).FirstOrDefaultAsync();
            if(admin == null)
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound,Message="Invalid username or password"});
            if (!admin.EmailConfirmed) return Ok(new ResponseDto<AuthDto> { Model = admin, Status = ResponseStatus.Unautorize, Message = "Email confirmation required" });

            try
            {
                var result = await PasswordHasher.VerifyPassword(loginDto.Password!, admin.PasswordSalt!, admin.PasswordHash!);
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                admin.IpAddress = HttpContext.Connection.RemoteIpAddress!.ToString();

                
                if(result)
                {
                    admin.LastLoginTime = DateTime.UtcNow;
                    admin.RefreshToken= _tokenManager.GenerateRefreshToken();
                    admin.TokenExpireTime=DateTime.UtcNow.AddHours(tokenExpireTimeHour);
                    _context.Adminstrators.Update(admin);
                    await _context.SaveChangesAsync();
                    AuthDto auth=admin;
                    auth.Token =await _tokenManager.GenerateToken(auth);
                    return Ok(new ResponseDto<AuthDto> { Model = auth, Status = ResponseStatus.Success });
                }
                else
                {
                    admin.LockCount += 1;
                    _context.Adminstrators.Update(admin);
                    await _context.SaveChangesAsync();

                    return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Unautorize,Message= "Invalid user name or password." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured: {ex.Message}" });
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
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

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RequestRefreshTokenDto tokenDto)
        {
            var tokenExpireTimeHour = 6;
           
            if (tokenDto is null)
            {
                return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = "Invalid client request" });
            }
            ClaimsPrincipal? principal;
            Claim? userClaim;
            AuthDto? user;
            try
            {
                principal = _tokenManager.GetPrincipalFromExpiredToken(tokenDto.Token);
                if (principal == null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Invalid token" });

                userClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
                if (userClaim == null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Claim not found" });

                var admin = await _context.Adminstrators.Include(a => a.Employee).Where(a => a.EmployeeId == userClaim.Value).FirstOrDefaultAsync();

                if (admin==null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "User not found" });

                 var ipAddress = HttpContext.Connection.RemoteIpAddress;
                if (!string.IsNullOrEmpty(admin.IpAddress) && admin.IpAddress!=ipAddress!.ToString())
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.Unautorize, Message = "address is not authorized" });


                if (!string.IsNullOrEmpty(tokenDto.RefreshToken) && admin.RefreshToken==tokenDto.RefreshToken)
                {
                    if (admin.TokenExpireTime < DateTime.UtcNow.AddMinutes(-5))
                    {
                        admin.RefreshToken = _tokenManager.GenerateRefreshToken();
                        admin.TokenExpireTime = DateTime.UtcNow.AddHours(tokenExpireTimeHour);
                    }
                   
                    admin.IpAddress = ipAddress!.ToString();
                    
                    _context.Adminstrators.Update(admin);
                    await _context.SaveChangesAsync();
                    AuthDto auth = admin;
                    auth.Token = await _tokenManager.GenerateToken(auth);
                    return Ok(new ResponseDto<AuthDto> { Model = auth, Status = ResponseStatus.Success });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = ex.Message });
            }
            return Ok(new ResponseDto<AuthDto> {Status = ResponseStatus.NotFound });
        }
    }
}
