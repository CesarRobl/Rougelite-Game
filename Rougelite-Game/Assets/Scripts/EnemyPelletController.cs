using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPelletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField]
    void Awake()
    {
        Invoke("ShootPellet", .1f);
        Invoke("FacePlayer", .1f);
    }

    void ShootPellet()
    {
        Vector2 dir = GMController.gm.player.position - transform.position;
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
        }
       
        else if (col.CompareTag("Walls") || col.CompareTag("Door") || col.CompareTag("Obstacle") ) Destroy(gameObject);
    }
}
