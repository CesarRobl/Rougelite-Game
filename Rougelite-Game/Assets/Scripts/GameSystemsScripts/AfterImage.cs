using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<SpriteRenderer>().color.a <= 0) Destroy(gameObject);
    }
}
