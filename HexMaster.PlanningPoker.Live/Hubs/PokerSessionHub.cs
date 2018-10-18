using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.PlanningPoker.Live.Hubs
{
    public class PokerSessionHub : Hub
    {

        protected IHubContext<PokerSessionHub> _context;


        public async Task SessionStarted(Guid pokerSessionId)
        {
            await _context.Clients.Group(pokerSessionId.ToString()).SendAsync("start");
        }
        public async Task ResetAll(Guid pokerSessionId)
        {
            await _context.Clients.Group(pokerSessionId.ToString()).SendAsync("reset");
        }
        public async Task ParticipantEstimated(Guid pokerSessionId, Guid participantId, decimal estimation)
        {
            await _context.Clients.Group(pokerSessionId.ToString()).SendAsync("participantEstimation", participantId, estimation);
        }
        public async Task ParticipantJoined(Guid pokerSessionId, Guid participantId, string displayName)
        {
            await _context.Clients.Group(pokerSessionId.ToString()).SendAsync("participantJoined", participantId, displayName);
        }
        public async Task ParticipantLeft(Guid pokerSessionId, Guid participantId)
        {
            await _context.Clients.Group(pokerSessionId.ToString()).SendAsync("participantLeft", participantId);
        }

        public async Task RegisterParticipant(string pokerSession)
        {
            await _context.Groups.AddToGroupAsync(Context.ConnectionId, pokerSession);
        }


        public PokerSessionHub(IHubContext<PokerSessionHub> context)
        {
            _context = context;
        }


    }
}
