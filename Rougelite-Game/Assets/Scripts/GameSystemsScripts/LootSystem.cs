using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public GameObject droppedLoot;
    public List<LootList> lootList = new List<LootList>();

    LootList GetDroppedItem()
    {
        int ran = Random.Range(1, 101);
        List<LootList> possibleLoot = new List<LootList>();
        foreach (LootList item in lootList)
        {
            if(ran <= item.dropchance)
            { 
               possibleLoot.Add(item);
            }
        }

        if (possibleLoot.Count > 0)
        {
            LootList droppedloot = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedloot;
        }
        Debug.Log("No Loot Dropped");
        return null;
    }

    public void SpawnLoot(Vector3 transform)
    {
        LootList droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootObject = Instantiate(droppedLoot, transform, Quaternion.identity);
            lootObject.GetComponent<SpriteRenderer>().sprite = droppedItem.sprite;
        }
    }
}
