using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator[] sword;
    public bool anidone;
    [SerializeField] private GameObject spatula;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpatulaSwipe()
    {
        Vector3 currentpos = spatula.transform.localPosition;
        Vector3 currentrot = spatula.transform.eulerAngles;
         sword[0].SetTrigger("Swing");
         Vector3 rot = new Vector3(0, 0, 44);
         spatula.transform.localPosition = new Vector3(currentpos.x, -1.5f, 0);
         // spatula.transform.Rotate(Quaternion.Euler());
        yield return new WaitForSeconds(10f);
        spatula.transform.localPosition = currentpos;
    }
}
