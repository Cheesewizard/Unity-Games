using UnityEngine;

namespace ScriptableObject.Items.Equipment
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public int attackBonus;
        public int defenceBonus;

        private void Awake()
        {
            type = ItemType.Equipment;
        }
    }
}