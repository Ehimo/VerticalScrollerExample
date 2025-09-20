using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections.Generic;
using UnityEngine;
using CustomPool;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Bullet _bulletPf;
    [SerializeField] private Transform _shootPosition;

    private EventBus _eventBus;
    
    private bool _shootControllerIsWorking = true;

    private bool _reloading = false;

    private float _timer = 0;

    private CustomPool<Bullet> _bulletPool;
    
    private void Start()
    {
        _bulletPool = new(_bulletPf, 10);

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStartedSignal>(x => { _shootControllerIsWorking = true; });

        _eventBus.Subscribe<GameStopSignal>(x => { _shootControllerIsWorking = false; });
    }

    private void Update()
    {
        if (!_shootControllerIsWorking)
            return;

        if (_reloading)
        {
            _timer += Time.deltaTime;
            if(_timer >= _player.ReloadTime)
            {
                Debug.Log("Перезяредился");
                _timer = 0;
                _reloading = false;
            }
            else
            {
                return;
            }
        }

        var shootButtonClicked = Input.GetKey(KeyCode.Space);

        if (shootButtonClicked && !_reloading)
        {
            Debug.Log("Стреляю!");
            
            // Получаем префаб из пула.
            var bullet = _bulletPool.Get();
            
            bullet.transform.position = _shootPosition.position;
            bullet.gameObject.SetActive(true);

            // Начинаем перезарядку.
            _reloading = true;
            Debug.Log("Начинаю перезарежатся.");
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStartedSignal>(x => { _shootControllerIsWorking = true; });

        _eventBus.Unsubscribe<GameStopSignal>(x => { _shootControllerIsWorking = false; });
    }
}
