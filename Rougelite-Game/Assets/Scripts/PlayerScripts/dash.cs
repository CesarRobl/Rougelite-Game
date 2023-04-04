using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private GameObject Effects;
    private bool CanDash = true;
    private bool IsDashing;
    private float DashingPower = 35f;
    private float DashingTime = 0.2f;
    private float DashingCoolDown = 1f;


    void Update()
    {
        Vector2 pos = GMController.gm.pos - GMController.gm.temp.transform.position;
        if (Input.GetMouseButtonDown(1) && CanDash)
        {
            StartCoroutine(Dash(GMController.gm.temp.walkingDir));
        }
        
    }

    private IEnumerator Dash(Vector2 dir)
    {
        CanDash = false;
        IsDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = dir.normalized * DashingPower;
        dust.Play();
        yield return new WaitForSeconds(DashingTime);
        rb.gravityScale = originalGravity;
        IsDashing = false;
        yield return new WaitForSeconds(DashingCoolDown);
        CanDash = true;
    }
}
