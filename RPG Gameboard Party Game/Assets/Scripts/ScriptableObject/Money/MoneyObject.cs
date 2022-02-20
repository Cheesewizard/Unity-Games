using ScriptableObject.Items;
using UnityEngine;

namespace ScriptableObject.Money
{
    [CreateAssetMenu(fileName = "New Money Object", menuName = "Inventory System/Items/Money")]
    public class MoneyObject : ItemObject
    {
        public int value;
        private void Awake()
        {
            type = ItemType.Money;
        }
    }
}
