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
    }

    void ShootPellet()
    {
        Vector2 dir = GMController.gm.player.position - transform.position;
        rb.velocity = dir.normalized * GMController.gm.pelletspeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null & !GMController.gm.playerhurt)
        {
            GMController.gm.playerhealth--;
            GMController.gm.playerhurt = true;
            Destroy(gameObject);
        }
       
        else if (col.CompareTag("Walls") || col.CompareTag("Door") ) Destroy(gameObject);
    }
}
