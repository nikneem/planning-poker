namespace HexMaster.Helpers.Infrastructure.Enums
{
    public enum TrackingState : byte
    {
        Unchanged = 0,
        Added = 1,
        Modified = 2,
        Deleted = 3,
        Touched = 4
    }
}