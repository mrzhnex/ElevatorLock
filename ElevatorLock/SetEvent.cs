using EXILED;
using EXILED.Extensions;

namespace ElevatorLock
{
    public class SetEvent
    {
        public void OnRoundStart()
        {
            Global.elevators = Map.Lifts;
        }

        public string GetUsageElock()
        {
            return "elock <all>(lock/unlock) | (elevator_name) | <get> = (return list of elevators name)";
        }

        public void OnRemoveAdminCommand(ref RACommandEvent ev)
        {
            string[] args = ev.Command.Split(' ');
            bool silent = false;
            if (args.Length < 2)
                return;
            if (args[0] != "elock")
            {
                return;
            }
            ev.Allow = false;
            if (args[1] == "all")
            {
                if (args.Length < 3)
                {
                    ev.Sender.RAMessage("Out of args. Usage: " + GetUsageElock(), false);
                    return;
                }
                bool locked;
                if (args[2].ToLower().Contains("unlock"))
                    locked = false;
                else if (args[2].ToLower().Contains("lock"))
                    locked = true;
                else
                {
                    ev.Sender.RAMessage("Out of args. Usage: " + GetUsageElock(), false);
                    return;
                }
                if (args.Length > 3 && args[3].ToLower() == "silent")
                    silent = true;
                foreach (Lift el in Global.elevators)
                {
                    el.Networklocked = locked;
                }
                string answer = "locked";
                if (!locked)
                    answer = "unlocked";
                if (!silent)
                {
                    if (locked)
                        NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ALL ELEVATOR SYSTEM HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                    else
                        NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ALL ELEVATOR SYSTEM HAS BEEN ACTIVATED", 0.0f, 0.0f);
                }
                ev.Sender.RAMessage("All elevators has been " + answer);
                return;
            }
            else if (args[1] == "get")
            {
                string answer = string.Empty;
                foreach (Lift el in Global.elevators)
                {
                    if (answer.Contains(el.elevatorName.ToString()))
                        continue;
                    answer = answer + el.elevatorName.ToString() + ", ";
                }

                ev.Sender.RAMessage("Names: " + answer);
                return;
            }
            else
            {
                if (args.Length > 3)
                {
                    ev.Sender.RAMessage("Wrong args. Usage: " + GetUsageElock(), false);
                    return;
                }
                string answer = string.Empty;
                bool locked = false;
                foreach (Lift el in Global.elevators)
                {
                    if (el.elevatorName.ToString().ToLower().Contains(args[1].ToLower()))
                    {
                        answer = answer + el.elevatorName + ", ";
                        el.Networklocked = !el.Networklocked;
                        locked = el.Networklocked;
                    }
                }
                if (answer == string.Empty)
                {
                    ev.Sender.RAMessage("Not found elevators_name: " + GetUsageElock(), false);
                    return;
                }
                if (args.Length > 2 && args[2].ToLower() == "silent")
                    silent = true;
                if (!silent)
                {
                    if (answer.ToLower().Contains("ela"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("LIGHT CONTAINMENT ZONE ELEVATOR SYSTEM A HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("LIGHT CONTAINMENT ZONE ELEVATOR SYSTEM A HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                    else if (answer.ToLower().Contains("elb"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("LIGHT CONTAINMENT ZONE ELEVATOR SYSTEM B HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("LIGHT CONTAINMENT ZONE ELEVATOR SYSTEM B HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                    else if (answer.ToLower().Contains("gatea"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ENTRANCE ZONE ELEVATOR SYSTEM A HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ENTRANCE ZONE ELEVATOR SYSTEM A HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                    else if (answer.ToLower().Contains("gateb"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ENTRANCE ZONE ELEVATOR SYSTEM B HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ENTRANCE ZONE ELEVATOR SYSTEM B HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                    else if (answer.ToLower().Contains("049"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("SCP 0 4 9 CONTAINMENT CHAMBER ELEVATOR SYSTEM HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("SCP 0 4 9 CONTAINMENT CHAMBER ELEVATOR SYSTEM HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                    else if (answer.ToLower().Contains("warhead"))
                    {
                        if (locked)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ALPHA WARHEAD ELEVATOR SYSTEM HAS BEEN DEACTIVATED", 0.0f, 0.0f);
                        else
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("ALPHA WARHEAD ELEVATOR SYSTEM HAS BEEN ACTIVATED", 0.0f, 0.0f);
                    }
                }

                ev.Sender.RAMessage("Change lock status for: " + answer);
                return;
            }
        }
    }
}