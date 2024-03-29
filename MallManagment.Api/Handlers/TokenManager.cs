﻿using MallManagment.Api.Settings;
using MallManagment.Models.Constants;
using MallManagment.Models.Dtos;
using MallManagment.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MallManagment.Api.Handlers
{
    public class TokenManager : ITokenManager
    {
        public JWTSettings jwtSettings { get; }
        public TokenManager(IOptions<JWTSettings> appSettings)
        {
            this.jwtSettings = appSettings.Value;

        }
        public async Task<string> GenerateToken(AuthDto admin)
        {
            try
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(admin);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return await Task.FromResult(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,

                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            //SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims(AuthDto admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.EmployeeId!),
                new Claim(ClaimTypes.Email, admin.Email!),
                new Claim(ClaimConstant.EMAIL_CONFIRMED, admin.EmailConfirmed.ToString()),
                new Claim(ClaimConstant.PHONE_CONFIRMED, admin.PhoneConfirmed.ToString()),
                new Claim(ClaimTypes.Role, admin.StringRole!),
            };
            
            if (!string.IsNullOrEmpty(admin.FullName))
                claims.Add(new Claim(ClaimTypes.Name, admin.FullName));

            return await Task.FromResult(claims);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.ExpiryInMinutes)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

    }
}
