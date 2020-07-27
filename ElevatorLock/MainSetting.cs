using Exiled.API.Features;

namespace ElevatorLock
{
    public class MainSetting : Plugin<Config>
    {
        public override string Name => nameof(ElevatorLock);
        public SetEvent SetEvent { get; set; }

        public override void OnEnabled()
        {
            Global.IsFullRp = Config.IsFullRp;
            Log.Info(nameof(Global.IsFullRp) + ": " + Global.IsFullRp);
            SetEvent = new SetEvent();
            Exiled.Events.Handlers.Map.ExplodingGrenade += SetEvent.OnExplodingGrenade;
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += SetEvent.OnSendingRemoteAdminCommand;
            Exiled.Events.Handlers.Player.InteractingElevator += SetEvent.OnInteractingElevator;
            Exiled.Events.Handlers.Server.WaitingForPlayers += SetEvent.OnWaitingForPlayers;
            Log.Info(Name + " on");
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade -= SetEvent.OnExplodingGrenade;
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= SetEvent.OnSendingRemoteAdminCommand;
            Exiled.Events.Handlers.Player.InteractingElevator -= SetEvent.OnInteractingElevator;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= SetEvent.OnWaitingForPlayers;
            Log.Info(Name + " off");
        }
    }
}