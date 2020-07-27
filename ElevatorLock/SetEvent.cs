using Exiled.API.Features;
using Exiled.Events.EventArgs;
using UnityEngine;
using System.Collections.Generic;

namespace ElevatorLock
{
    public class SetEvent
    {
        private string GetUsageElock()
        {
            return "elock <all>(lock/unlock) | (elevator_name) | <get> = (return list of elevators name)";
        }

        internal void OnSendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            bool silent = false;
            if (ev.Name.ToLower() != "elock")
                return;
            ev.IsAllowed = false;
            if (ev.Arguments.Count < 1)
            {
                ev.Sender.RemoteAdminMessage("Out of args. Usage: " + GetUsageElock(), false);
                return;
            }
            if (ev.Arguments[0] == "all")
            {
                if (ev.Arguments.Count < 2)
                {
                    ev.Sender.RemoteAdminMessage("Out of args. Usage: " + GetUsageElock(), false);
                    return;
                }
                bool locked;
                if (ev.Arguments[1].ToLower().Contains("unlock"))
                    locked = false;
                else if (ev.Arguments[1].ToLower().Contains("lock"))
                    locked = true;
                else
                {
                    ev.Sender.RemoteAdminMessage("Out of args. Usage: " + GetUsageElock(), false);
                    return;
                }
                if (ev.Arguments.Count > 3 && ev.Arguments[2].ToLower() == "silent")
                    silent = true;
                foreach (Lift el in Map.Lifts)
                {
                    el.Network_locked = locked;
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
                ev.Sender.RemoteAdminMessage("All elevators has been " + answer);
                return;
            }
            else if (ev.Arguments[0] == "get")
            {
                string answer = string.Empty;
                foreach (Lift el in Map.Lifts)
                {
                    if (answer.Contains(el.elevatorName.ToString()))
                        continue;
                    answer = answer + el.elevatorName.ToString() + ", ";
                }

                ev.Sender.RemoteAdminMessage("Names: " + answer);
                return;
            }
            else
            {
                if (ev.Arguments.Count > 2)
                {
                    ev.Sender.RemoteAdminMessage("Wrong args. Usage: " + GetUsageElock(), false);
                    return;
                }
                string answer = string.Empty;
                bool locked = false;
                foreach (Lift el in Map.Lifts)
                {
                    if (el.elevatorName.ToString().ToLower().Contains(ev.Arguments[0].ToLower()))
                    {
                        answer = answer + el.elevatorName + ", ";
                        el.Network_locked = !el.Network_locked;
                        locked = el.Network_locked;
                    }
                }
                if (answer == string.Empty)
                {
                    ev.Sender.RemoteAdminMessage("Not found elevators_name: " + GetUsageElock(), false);
                    return;
                }
                if (ev.Arguments.Count > 1 && ev.Arguments[1].ToLower() == "silent")
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

                ev.Sender.RemoteAdminMessage("Change lock status for: " + answer);
                return;
            }
        }

        internal void OnWaitingForPlayers()
        {
            Global.BrokenElevators = new List<Lift.Elevator>();
        }

        internal void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (Global.IsFullRp && Global.BrokenElevators.Contains(ev.Elevator))
                ev.IsAllowed = false;
        }

        internal void OnExplodingGrenade(ExplodingGrenadeEventArgs ev)
        {
            if (!Global.IsFullRp || !ev.IsFrag)
                return;

            foreach (Lift lift in Map.Lifts)
            {
                foreach (Lift.Elevator elevator in lift.elevators)
                {
                    if (Vector3.Distance(ev.Grenade.gameObject.transform.position, elevator.door.gameObject.transform.position) < Global.DistanceToDestroyElevator)
                    {
                        if (!Global.BrokenElevators.Contains(elevator))
                        {
                            Global.BrokenElevators.AddRange(lift.elevators);
                        }
                    }
                }
            }
        }
    }
}