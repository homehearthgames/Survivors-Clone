using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Drop Table", menuName = "Drop Table", order = 51)]
public class DropTable : ScriptableObject
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject item; // The prefab of the item to drop
        public float dropChance; // The drop chance in percent
    }

    public List<DropItem> items = new List<DropItem>(); // The list of possible drops
    public float chanceToDropNothing; // The chance to drop nothing at all
}
