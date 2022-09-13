using AutoMapper;
using DM.DAL.Models;
using Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Authorization
{
    public class AccountService
    {
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IMapper? _mapper;
        public AccountService(IDbContextFactory<DMContext>? contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<AccountDTO>? GetByUserName(string userName)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                Account? account =await context.Accounts.Where(x => x.UserName == userName).FirstOrDefaultAsync();
                if (account == null)
                {
                    return null;
                }
                else
                {
                    account = await context.Accounts.Where(x => x.UserName == userName).Include(p => p.Person).SingleAsync();
                }

                return _mapper.Map<AccountDTO>(account);
            }
        }
        public async Task<AccountDTO>? GetByPersonId(int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                Account? account =await context.Accounts.Where(x => x.PersonId == personId).Include(p => p.Person).SingleAsync();

                return _mapper.Map<AccountDTO>(account);
            }
        }
        public async Task<AccountDTO>? GetItemById(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            Account? account = await context.Accounts.FindAsync(id);

            return _mapper.Map<AccountDTO>(account);

        }
        public async Task<AccountDTO> AddAccount(AccountDTO accountDTO)
        {
            using var context = _contextFactory.CreateDbContext();

            Account account = new Account
            {
                UserName = accountDTO.UserName,
                Role=accountDTO.Role,
                Password=accountDTO.Password,
                PersonId=accountDTO.PersonId,
            };
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();

            return await GetItemById(account.Id);
        }


        public async Task UpdatePassword(AccountDTO accountDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Accounts.Find(accountDTO.Id);
            if (element is null)
            {
                element = new Account();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Password = accountDTO.Password;
            context.Accounts.Update(element);
            await context.SaveChangesAsync();

        }

        public async Task UpdateRole(AccountDTO accountDTO)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Accounts.Find(accountDTO.Id);
            if (element is null)
            {
                element = new Account();
                throw new ArgumentNullException($"Unknown {element.GetType().Name}");
            }
            element.Role = accountDTO.Role;
            context.Accounts.Update(element);
            await context.SaveChangesAsync();

        }
        public async Task<bool> NameIsBusy(string username)
        {
            using var context = _contextFactory.CreateDbContext();
            var element = context.Accounts.FirstOrDefault(a => a.UserName == username);
            if (element is null)
            {
                return false;
            }
            return true;
        }




        public void Dispose()
        {
            
        }
    }
}
