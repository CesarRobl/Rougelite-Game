using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : TestAI
{
   
    void Awake()
    {
        Setup();
    }

    
    void Update()
    {
        SeekPlayer();
        
    }
}
