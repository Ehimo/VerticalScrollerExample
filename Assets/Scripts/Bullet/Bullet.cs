using UnityEngine;

/// <summary>
/// Статистика пуль. Скорость, урон и т.д
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _moveSpeed = 100;
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private int _bulletDamage = 1;

    public int MoveSpeed => _moveSpeed;
    public float SpeedMultiplier => _speedMultiplier;
    public int BulletDamage => _bulletDamage;

}
