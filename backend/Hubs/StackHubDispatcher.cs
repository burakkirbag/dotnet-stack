using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace stack.Hubs
{
    public interface IStackHubDispatcher
    {
        Task SendMessage(string message);
    }

    public class StackHubDispatcher : IStackHubDispatcher
    {
        private readonly IHubContext<StackHub> _hubContext;

        public StackHubDispatcher(IHubContext<StackHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
