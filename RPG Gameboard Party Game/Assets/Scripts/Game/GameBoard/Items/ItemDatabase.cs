using Mirror;
using ScriptableObject;
using ScriptableObject.Items;
using UnityEngine;

namespace Game.GameBoard.Items
{
    public class ItemDatabase : NetworkBehaviour
    {
        public static ItemDatabase Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public AssetList allWeapons;
        public AssetList allArmour;
        public AssetList allFood;
        public AssetList alldefaultItems;
        public AssetList allMoney;


        public readonly SyncDictionary<string, ItemObject>
            itemObjects = new SyncDictionary<string, ItemObject>();

        private void Start()
        {
            if (!isServer) return;
            
            foreach (var item in allWeapons.assets)
            {
                itemObjects.Add(item.id, item);
                Debug.Log($"Item {item.id} {item.name} {item.description} Added");
            }

            foreach (var item in allArmour.assets)
            {
                itemObjects.Add(item.id, item);
                Debug.Log($"Item {item.id} {item.name} {item.description} Added");
            }

            foreach (var item in allFood.assets)
            {
                itemObjects.Add(item.id, item);
                Debug.Log($"Item {item.id} {item.name} {item.description} Added");
            }

            foreach (var item in alldefaultItems.assets)
            {
                itemObjects.Add(item.id, item);
                Debug.Log($"Item {item.id} {item.name} {item.description} Added");
            }

            foreach (var item in allMoney.assets)
            {
                itemObjects.Add(item.id, item);
                Debug.Log($"Item {item.id} {item.name} {item.description} Added");
            }
        }

        public ItemObject GetItem(string itemId)
        {
            if (itemObjects.ContainsKey(itemId))
            {
                return itemObjects[itemId];
            }

            return null;
        }
    }
}