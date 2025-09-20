using CustomEventBus;
using CustomEventBus.Signals;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class BulletMovementController : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Rigidbody2D _rb;

    private const int timeToDispawn = 10;

    private bool _gameNotStopped = true;

    private EventBus _eventBus;
    private async void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStopSignal>(x => { _gameNotStopped = false; });

        _eventBus.Subscribe<GameStartedSignal>(x => { _gameNotStopped = true; });

        StartCoroutine(DispawnCoroutine());
    }

    private IEnumerator DispawnCoroutine()
    {
        for(float i = 0; i < timeToDispawn; i += Time.deltaTime)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_gameNotStopped)
            return;

        _rb.velocity = Vector2.up * _bullet.MoveSpeed * _bullet.SpeedMultiplier * Time.deltaTime;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStopSignal>(x => { _gameNotStopped = false; });

        _eventBus.Unsubscribe<GameStartedSignal>(x => { _gameNotStopped = true; });

        StopAllCoroutines();
    }
}
