using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPelletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform loc;
    void Awake()
    {
       ShootPellet();
        // Invoke("FacePlayer", .1f);
    }

    void ShootPellet()
    {
        Vector2 dir = loc.position - transform.position;
        rb.velocity = dir.normalized * GMController.gm.pelletspeed;
    }

    void FacePlayer()
    {
       
        Vector3 pos = GMController.gm.temp.transform.position;
        Vector2 dir = pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null  )
        {
            if(!GMController.gm.playerhurt)GMController.gm.temp.Playerhurt();
            Destroy(gameObject);
            Debug.Log("hit");
        }
       
        else if (col.CompareTag("Walls") || col.CompareTag("Door") || col.CompareTag("Obstacle") ) Destroy(gameObject);
    }
}
