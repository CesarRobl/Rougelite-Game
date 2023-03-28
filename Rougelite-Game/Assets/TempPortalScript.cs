using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPortalScript : MonoBehaviour
{
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxGrowth;
    private Vector3 growth;
    private Transform size;
    private bool done;
    void Awake()
    {
        growth = new Vector3(maxGrowth, maxGrowth, 1);
        size = GetComponent<Transform>();
    }

    
    void Update()
    {
        if(!done)StartCoroutine(PortalGrow());
    }

    IEnumerator PortalGrow()
    {
        Debug.Log("change size");
        size.localScale = Vector3.MoveTowards(size.localScale, growth, growSpeed * Time.deltaTime);
        yield return new WaitUntil(()=> size.localScale.x >= maxGrowth);
        GetComponent<BoxCollider2D>().enabled = true;
        done = true;
    }
}
