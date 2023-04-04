using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPortalScript : MonoBehaviour
{
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxGrowth;
    private Vector3 growth;
    private Transform size;
    [HideInInspector]public bool done;
    void Awake()
    {
        growth = new Vector3(maxGrowth, maxGrowth, 1);
        size = GetComponent<Transform>();
    }

    
    void Update()
    {
        
    }

    public IEnumerator PortalGrow()
    {
        size.localScale = Vector3.MoveTowards(size.localScale, growth, growSpeed * Time.deltaTime);
        yield return new WaitUntil(()=> size.localScale.x >= maxGrowth);
        GetComponent<BoxCollider2D>().enabled = true;
        done = true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TestPlayer"))
        {
            SceneManager.LoadScene("RandomLevel");
        }
    }
    
}
