using UnityEngine;

[System.Serializable]
public struct ShipData
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _id;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _purchasePrice;
    [SerializeField] private Sprite _shipSprite;
    [SerializeField] private float _shootSpeed;

    public int MaxHealth => _maxHealth;
    public int ID => _id;
    public float MovementSpeed => _movementSpeed;
    public int PurchasePrice => _purchasePrice;
    public Sprite ShipSprite => _shipSprite;
    public float ShootSpeed => _shootSpeed;
    
    public ShipData(int maxHealth, int id, float movementSpeed, int purchasePrice, Sprite shipSprite, float shootSpeed)
    {
        _maxHealth = maxHealth;
        _id = id;
        _movementSpeed = movementSpeed;
        _purchasePrice = purchasePrice;
        _shipSprite = shipSprite;
        _shootSpeed = shootSpeed;
    }
}