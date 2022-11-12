using MallManagment.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Api.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseDto<AuthDto>> VerifyUser(LoginDto dto);
    }
}
