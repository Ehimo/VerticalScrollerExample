using Newtonsoft.Json;
using UnityEngine;

namespace ConfigLoader.Ship.JSON
{
    public struct JsonShipData
    {
        [JsonProperty] [SerializeField] private int maxHealth;
        [JsonProperty] [SerializeField] private int id;
        [JsonProperty] [SerializeField] private float movementSpeed;
        [JsonProperty] [SerializeField] private int purchasePrice;
        [JsonProperty] [SerializeField] private string shipSprite;
        [JsonProperty][SerializeField] private float shootSpeed;

        public int MaxHealth => maxHealth;
        public int ID => id;
        public float MovementSpeed => movementSpeed;
        public int PurchasePrice => purchasePrice;
        public string ShipSprite => shipSprite;
        public float ShootSpeed => shootSpeed;
    }
}