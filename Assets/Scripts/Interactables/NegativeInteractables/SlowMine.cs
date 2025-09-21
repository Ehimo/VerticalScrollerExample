using CustomEventBus.Signals;

namespace Interactables
{
    public class SlowMine : DamageMine
    {
        protected override void Interact()
        {
            base.Interact();
            _eventBus.Invoke(new PlayerSlowSignal(1.5f, 10));
        }
    }
}