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
        private readonly DMContext? _db;
        private readonly IMapper? _mapper;
        public AccountService(DMContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        public AccountDTO? GetByUserName(string userName)
        {
            Account? account = _db.Accounts.Include(p=>p.Person).FirstOrDefault(x => x.UserName == userName);
            
            return _mapper.Map<AccountDTO>(account);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
