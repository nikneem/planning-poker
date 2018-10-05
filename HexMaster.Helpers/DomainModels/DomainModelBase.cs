using System;
using System.Collections.Generic;
using System.Text;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.Helpers.DomainModels
{
    public class DomainModelBase<T>
    {
        public T Id { get; }
        public TrackingState State { get; private set; }

        public DomainModelBase(T id, TrackingState state = TrackingState.Unchanged)
        {
            Id = id;
            State = state;
        }

        protected virtual void SetState(TrackingState state)
        {
            switch (State)
            {
                case TrackingState.Unchanged:
                    State = state;
                    break;
                case TrackingState.Touched:
                    if (State != TrackingState.Unchanged)
                    {
                        State = state;
                    }
                    break;
                case TrackingState.Modified:
                case TrackingState.Deleted:
                case TrackingState.Added:
                    if (State != TrackingState.Added && State != TrackingState.Deleted)
                    {
                        State = state;
                    }
                    break;
            }
        }

    }
}
