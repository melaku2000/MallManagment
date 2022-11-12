using MallManagment.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Api.Interfaces
{
    public interface IBankRepository
    {
        Task<ResponseDto<BankDto>> CreateAsync(BankDto dto);
        Task<ResponseDto<BankDto>> GetByIdsync(string id);
        Task<ResponseDto<List<BankDto>>> GetListsync();
        Task<ResponseDto<BankDto>> Updatesync(BankDto dto);
        Task<ResponseDto<BankDto>> Deletesync(string id);
    }
}
