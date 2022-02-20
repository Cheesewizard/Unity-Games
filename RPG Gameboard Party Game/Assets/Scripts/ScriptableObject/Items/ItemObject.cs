using UnityEngine;

namespace ScriptableObject.Items
{
    public enum ItemType
    {
        Food,
        Equipment,
        Default,
        Money
    }

    public class ItemObject : UnityEngine.ScriptableObject
    {
        [ScriptableObjectId]
        public string id;

        [Header("Item data")] public GameObject prefab;
        
        // Lookup this value on the client i.e sprite dictionary to load the correct sprite for the item.
        public int sprite;
        public ItemType type;
        public string name;
        [TextArea(15, 20)] public string description;
    }
}