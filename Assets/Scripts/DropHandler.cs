using UnityEngine;

public class DropHandler : MonoBehaviour
{
    public DropTable dropTable; // The drop table to use


public void DropItems()
    {
        // Calculate the total drop chance based on the items in the drop table.
        float totalChance = dropTable.chanceToDropNothing;
        foreach (var dropItem in dropTable.items)
        {
            totalChance += dropItem.dropChance;
        }

        // Choose a random value between 0 and the total drop chance.
        float randomValue = Random.Range(0, totalChance);

        // If the random value is less than the chance to drop nothing, don't drop an item.
        if (randomValue < dropTable.chanceToDropNothing)
        {
            return;
        }
        
        // Adjust the random value to exclude the chance to drop nothing.
        randomValue -= dropTable.chanceToDropNothing;

        // Try to find an item to drop.
        foreach (var dropItem in dropTable.items)
        {
            if (randomValue < dropItem.dropChance)
            {
                Instantiate(dropItem.item, transform.position, Quaternion.identity);
                return;
            }
            randomValue -= dropItem.dropChance;
        }

        // If no item is chosen, nothing is dropped.
    }
}