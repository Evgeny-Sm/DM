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
                Account? account = context.Accounts.Where(x => x.UserName == userName).Include(p => p.Person).Single();

                return _mapper.Map<AccountDTO>(account);
            }
        }
        public async Task<AccountDTO>? GetByPersonId(int personId)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                Account? account = context.Accounts.Where(x => x.PersonId == personId).Include(p => p.Person).Single();

                return _mapper.Map<AccountDTO>(account);
            }
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




        public void Dispose()
        {
            
        }
    }
}
