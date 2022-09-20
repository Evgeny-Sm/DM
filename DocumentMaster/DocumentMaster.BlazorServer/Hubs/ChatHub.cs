using DM.BLL.Services;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace DocumentMaster.BlazorServer.Hubs
{
    public class ChatHub:Hub
    {
        private readonly MessageService _messageService;
        public ChatHub(MessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await _messageService.AddItemAsync(new UserMessage
            {
                UserName = user,
                Message = message,
                DateSend = DateTime.Now
            });

        }
    }
}
