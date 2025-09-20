using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class DamageMine : InteractableWhoCollideWithBullet
    {
        [SerializeField] private int _damageValue = 1;
        protected override void Interact()
        {
            _eventBus.Invoke(new PlayerDamagedSignal(_damageValue));
        }

        protected override void InteractWithBullet()
        {
            Debug.Log("Mine destroyed by bullet");
            _eventBus.Invoke(new SpawnInteractableInSpecialPosition(InteractableType.GOLD, 0, transform));
            gameObject.SetActive(false);
        }

    }
}