using EXILED;

namespace ElevatorLock
{
    public class MainSetting : Plugin
    {
        public override string getName => "ElevatorLock";
        private SetEvent SetEvent;

        public override void OnEnable()
        {
            SetEvent = new SetEvent();
            Events.RoundStartEvent += SetEvent.OnRoundStart;
            Events.RemoteAdminCommandEvent += SetEvent.OnRemoveAdminCommand;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= SetEvent.OnRoundStart;
            Events.RemoteAdminCommandEvent -= SetEvent.OnRemoveAdminCommand;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}