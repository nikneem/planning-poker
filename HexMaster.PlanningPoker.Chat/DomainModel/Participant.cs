using System;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.PlanningPoker.Chat.DomainModel
{
    public sealed class Participant : DomainModelBase<Guid>
    {
        
        public string Name { get; private set; }

        public Participant(Guid id, string name, TrackingState state = TrackingState.Unchanged) : base(id, state)
        {
            Name = name;
        }
    }
}
