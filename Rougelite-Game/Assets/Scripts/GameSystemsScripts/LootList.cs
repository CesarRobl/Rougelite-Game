using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootList : ScriptableObject
{
   public Sprite sprite;
   public string lootname;
   public int dropchance;

   public LootList(string lootname, int dropchance)
   {
      this.lootname = lootname;
      this.dropchance = dropchance;
   }

}
