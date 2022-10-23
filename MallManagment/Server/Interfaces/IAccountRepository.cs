using MallManagment.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Server.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseDto<AuthDto>> VerifyUser(LoginDto dto);
    }
}
