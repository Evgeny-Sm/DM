using DM.BLL.Services;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace DocumentMaster.BlazorServer.Hubs
{
    public class NoteHub:Hub
    {
        public async Task SendMessage(int questId,string user, string message)
        {
            await Clients.All.SendAsync("ReceiveNote", questId, user, message);
        }
    }
}
