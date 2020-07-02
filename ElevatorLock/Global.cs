using System.Collections.Generic;

namespace ElevatorLock
{
    class Global
    {
        public static bool el_is_full_rp = true;
        public static List<Lift> elevators = new List<Lift>();
        public static float distance_to_destroy_lift = 5.0f;
    }
}