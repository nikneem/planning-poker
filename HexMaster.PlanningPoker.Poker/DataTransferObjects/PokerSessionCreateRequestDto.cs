using HexMaster.PlanningPoker.Poker.Infrastructure.Enums;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class PokerSessionCreateRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SessionName { get; set; }
        public ControlType ControlType { get; set; }
        public StartType StartType { get; set; }
    }
}