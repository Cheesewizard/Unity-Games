using ScriptableObject.Items;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "New Asset List", menuName = "List/Assets")]
    public class AssetList : UnityEngine.ScriptableObject
    {
        [SerializeField] 
        public ItemObject[] assets;
    }
}