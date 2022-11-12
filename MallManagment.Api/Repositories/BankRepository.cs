using MallManagment.Api.Data;
using MallManagment.Api.Interfaces;
using MallManagment.Models.Dtos;
using MallManagment.Models.Enums;
using MallManagment.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallManagment.Api.Repositories
{
    public class BankRepository : IBankRepository
    {
        public AppDbContext _context { get; }
        public BankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<BankDto>> CreateAsync(BankDto dto)
        {
            var current = DateTime.UtcNow;
            var model = new Bank
            {
                Id = Guid.NewGuid().ToString(),
                BankName = dto.BankName,
                BranchName = dto.BranchName,
                AccountNumber = dto.AccountNumber,
                ContactPerson = dto.ContactPerson,
                ContactMobilePerson = dto.ContactMobilePerson,
                Email = dto.Email,
                OfficePhone = dto.OfficePhone,
                OfficeAddress = dto.OfficeAddress,
                CreatedDate = current,
                ModifyDate = current
            };

            _context.Banks.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<BankDto> { Model = model, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<BankDto> { Model = model, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<BankDto>> GetByIdsync(string id)
        {
            var edu = await _context.Banks
               .FirstOrDefaultAsync(x => x.Id == id);
            if (edu == null)
                return new ResponseDto<BankDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<BankDto> { Model = edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<BankDto>>> GetListsync()
        {
            var banks = await _context.Banks
                .Select(a => (BankDto)a).ToListAsync();
            return new ResponseDto<List<BankDto>> { Model = banks, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<BankDto>> Updatesync(BankDto dto)
        {
            var bank = await _context.Banks.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (bank == null)
                return new ResponseDto<BankDto> { Status = ResponseStatus.NotFound };

            var current = DateTime.UtcNow;

            bank.BankName = dto.BankName;
            bank.BranchName = dto.BranchName;
            bank.Email = dto.Email;
            bank.ContactPerson = dto.ContactPerson;
            bank.ContactMobilePerson = dto.ContactMobilePerson;
            bank.AccountNumber = dto.AccountNumber;
            bank.OfficePhone = dto.OfficePhone;
            bank.OfficeAddress = dto.OfficeAddress;
            bank.ModifyDate = current;
            _context.Banks.Update(bank);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<BankDto>> Deletesync(string id)
        {
            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);

            if (bank == null)
                return new ResponseDto<BankDto> { Status = ResponseStatus.NotFound };
            _context.Banks.Remove(bank);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<BankDto> { Model = bank, Status = ResponseStatus.Success };
        }
    }
}
