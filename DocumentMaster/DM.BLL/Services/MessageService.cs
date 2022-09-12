using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class MessageService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        public MessageService(IMapper mapper, IDbContextFactory<DMContext>? contextFactory)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
        }
        public async Task<List<UserMessage>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var messages = await context.Messages.ToListAsync();
            var result = _mapper.Map<List<UserMessage>>(messages);
            return result;
        }

        public async Task<List<UserMessage>> GetItemsLast2MonthAsync()
        {
            var dateFrom=DateTime.Now.AddDays(-60);
            using var context = _contextFactory.CreateDbContext();
            var messages = await context.Messages.Where(m=>m.DateSend>dateFrom).ToListAsync();
            var result = _mapper.Map<List<UserMessage>>(messages);
            return result;
        }
        public async Task AddItemAsync(UserMessage mes)
        {
            using var context = _contextFactory.CreateDbContext();
            SendedMessage sendedMessage = new SendedMessage
            {
                Message = mes.Message,
                DateSend=mes.DateSend,
                UserName=mes.UserName,
            };
            await context.Messages.AddAsync(sendedMessage);
            await context.SaveChangesAsync();
      
        }





    }
}
