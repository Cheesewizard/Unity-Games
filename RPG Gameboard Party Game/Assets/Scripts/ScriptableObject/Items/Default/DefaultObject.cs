using UnityEngine;

namespace ScriptableObject.Items.Default
{
    [CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
    public class DefaultObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Default;
        }
    }
}