using DM.BLL.Services;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace DocumentMaster.BlazorServer.Hubs
{
    public class NoteHub:Hub
    {
        private readonly NoteService _noteService;
        public NoteHub(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task SendMessage(int questId,string user, string message, string fileName)
        {
            var noteDTO= await _noteService.AddItemAsync(new NoteDTO
            {
                UserName = user,
                Content = message,
                QuestionId = questId,
                DateTime = DateTime.Now,
                HasFile = !string.IsNullOrEmpty(fileName),
                Path = fileName
            });

            await Clients.All.SendAsync("ReceiveNote", questId, user, message, fileName,noteDTO.Id);


        }
    }
}
