using System.Collections.Generic;

namespace ElevatorLock
{
    public static class Global
    {
        public static bool IsFullRp = false;
        public static float DistanceToDestroyElevator = 7.0f;
        public static List<Lift.Elevator> BrokenElevators = new List<Lift.Elevator>();
    }
}