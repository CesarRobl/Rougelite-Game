using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomlist : MonoBehaviour
{
    public static Roomlist rl;
    
    public GameObject[] uprooms;
    public GameObject[] bottomrooms;
    public GameObject[] leftrooms;
    public GameObject[] rightrooms;
    public GameObject[] cornerrooms;
    public GameObject bossroom;

    public GameObject[] longUD;
    public GameObject[] longLR;

    private void Start()
    {
        rl = this;
    }
}
