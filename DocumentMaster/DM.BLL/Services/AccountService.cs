using AutoMapper;
using DM.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
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

        public async Task<AccountDTO> FindAccount(AccountDTO account)
        {
            var acc = _db.Persons.FirstOrDefault(u => u.Login == account.Login && u.Password == account.Password);
            if (acc != null)
            {
                var result = _mapper.Map<AccountDTO>(acc);
                return result;
            }
            return null;

        }

    }
}
