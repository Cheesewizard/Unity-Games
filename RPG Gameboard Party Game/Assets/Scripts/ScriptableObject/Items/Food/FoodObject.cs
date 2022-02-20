using ScriptableObject.Items;
using UnityEngine;

namespace ScriptableObject.Food
{
    
    [CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
    public class FoodObject : ItemObject
    {  public int restoreHealthValue;
        private void Awake()
        {
            type = ItemType.Food;
        }
    }
}