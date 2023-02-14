using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;

    public SoundControl SC;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();        
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        SC.PlaySteps();
    }
}

