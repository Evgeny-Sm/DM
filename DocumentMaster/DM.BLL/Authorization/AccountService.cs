using AutoMapper;
using DM.DAL.Models;
using Models;
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
            Person? person = _db.Persons.FirstOrDefault(x => x.Login == userName);
            
            return _mapper.Map<AccountDTO>(person);
        }
    }
}
