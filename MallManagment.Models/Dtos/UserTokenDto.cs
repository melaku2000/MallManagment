using MallManagment.Models.Enums;
using MallManagment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Models.Dtos
{
    public class UserTokenDto : BaseModel
    {
        public static implicit operator UserTokenDto(UserToken model)
        {
            return new UserTokenDto
            {
                Id = model.Id,
                AdminId = model.AdminId,
                Token = model.Token,
                TokenType = model.TokenType,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
                TokenExpireTime = model.TokenExpireTime
            };
        }
        public static implicit operator UserToken(UserTokenDto model)
        {
            return new UserToken
            {
                Id = model.Id,
                AdminId = model.AdminId,
                Token = model.Token,
                TokenType = model.TokenType,
                CreatedDate = model.CreatedDate,
                ModifyDate = model.ModifyDate,
                TokenExpireTime = model.TokenExpireTime
            };
        }
        public string? AdminId { get; set; }
        public string? Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}
