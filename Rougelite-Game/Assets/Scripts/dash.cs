using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private bool CanDash = true;
    private bool IsDashing;
    private float DashingPower = 24f;
    private float DashingTime = 0.2f;
    private float DashingCoolDown = 1f;


    void Update()
    { 
        if (Input.GetMouseButtonDown(1) && CanDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        yield return new WaitForSeconds(DashingTime);
        rb.gravityScale = originalGravity;
        IsDashing = false;
        yield return new WaitForSeconds(DashingCoolDown);
        CanDash = true;
    }
}
