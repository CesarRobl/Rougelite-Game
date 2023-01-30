using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 dir;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = GMController.gm.dir * new Vector2(GMController.gm.pelletspeed, GMController.gm.pelletspeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TestAI ai = col.gameObject.GetComponent<TestAI>();
        if (ai != null)
        {
            ai.HP--;
            Destroy(gameObject);
        }
    }
}
