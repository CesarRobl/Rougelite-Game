using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObstacleList : ScriptableObject
{
   public Sprite sprite;
   public Sprite spriteAlt;
   public string objectName;
   public int chance;

   public ObstacleList(string name, int spawnchance)
   {
      this.objectName = name;
      this.chance = spawnchance;
   }

}
