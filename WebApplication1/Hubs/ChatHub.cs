using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
        }

        public async Task Connect(string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }
    }
}
