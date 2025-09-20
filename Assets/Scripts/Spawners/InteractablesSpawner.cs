using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using CustomPool;
using Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// При поступлении сигнала Spawn достаёт нужный объект из пула
/// Или генирирует новый
/// </summary>
public class InteractablesSpawner : MonoBehaviour, IService
{
    [SerializeField] private Transform _parent;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _defaultY;
    [SerializeField] private InteractableConfig _config;
    
    public float MinX => _minX;
    public float MaxX => _maxX;

    private readonly Dictionary<string, CustomPool<Interactable>> _pools =
        new Dictionary<string, CustomPool<Interactable>>();

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Subscribe<SpawnInteractableInSpecialPosition>(SpawnInSpecialPosition);
        _eventBus.Subscribe<DisposeInteractableSignal>(Dispose);
    }

    private void Spawn(SpawnInteractableSignal signal)
    {
        var item = SpawnBasics(signal);
        item.transform.position = RandomizeSpawnPosition();

        ActiveInteractable(item);
    }

    private void SpawnInSpecialPosition(SpawnInteractableInSpecialPosition signal)
    {
        var item = SpawnBasics(signal);
        item.transform.position = signal.SpawnPosition.position;

        ActiveInteractable(item);
    }

    private Interactable SpawnBasics(SpawnInteractableSignal signal)
    {
        var interactable = _config.Get(signal.InteractableType, signal.Grade);
        var pool = GetPool(interactable);

        var item = pool.Get();
        item.transform.parent = _parent;

        return item;
    }

    private Interactable SpawnBasics(SpawnInteractableInSpecialPosition signal)
    {
        var interactable = _config.Get(signal.InteractableType, signal.Grade);
        var pool = GetPool(interactable);

        var item = pool.Get();
        item.transform.parent = _parent;

        return item;
    }

    private void ActiveInteractable(Interactable item)
    {
        _eventBus.Invoke(new InteractableActivatedSignal(item));
    }

    private void Dispose(DisposeInteractableSignal signal)
    {
        var interactable = signal.Interactable;
        var pool = GetPool(interactable);
        pool.Release(interactable);

        _eventBus.Invoke(new InteractableDisposedSignal(interactable));
    }

    private CustomPool<Interactable> GetPool(Interactable interactable)
    {
        var objectTypeStr = interactable.GetType().ToString();
        CustomPool<Interactable> pool;

        // Такого пула нет - создаём новый пул
        if (!_pools.ContainsKey(objectTypeStr))
        {
            pool = new CustomPool<Interactable>(interactable, 5);
            _pools.Add(objectTypeStr, pool);
        }
        // Пул есть - возвращаем пул
        else
        {
            pool = _pools[objectTypeStr];
        }

        return pool;
    }

    private Vector3 RandomizeSpawnPosition()
    {
        float x = Random.Range(_minX, _maxX);
        return new Vector3(x, _defaultY, 0);
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<SpawnInteractableSignal>(Spawn);
        _eventBus.Unsubscribe<SpawnInteractableInSpecialPosition>(SpawnInSpecialPosition);
        _eventBus.Unsubscribe<DisposeInteractableSignal>(Dispose);
    }
}