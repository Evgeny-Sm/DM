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
        public AccountDTO? GetByUserName(string userName)
        {
            using var context = _contextFactory.CreateDbContext();
            {
                Account? account = context.Accounts.Include(p => p.Person).FirstOrDefault(x => x.UserName == userName);

                return _mapper.Map<AccountDTO>(account);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
