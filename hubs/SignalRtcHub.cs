
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace signalRtc.hubs
{
    public class SignalRtcHub : Hub
    {
        public async Task SendOffer(string user, string offer)
        {
            await Clients.Others.SendAsync("ReceiveOffer", user, offer);
            Console.WriteLine("ReceiveOffer");
        }

        public async Task SendAnswer(string user, string answer)
        {
            await Clients.Others.SendAsync("ReceiveAnswer", user, answer);
            Console.WriteLine("ReceiveAnswer");
        }

        public async Task SendIceCandidate(string user, string candidate)
        {
            await Clients.Others.SendAsync("ReceiveIceCandidate", user, candidate);
            Console.WriteLine("ReceiveIceCandidate");
        }
    }
}
