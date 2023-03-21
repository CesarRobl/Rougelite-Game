using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    public Animator[] sword;
    public bool anidone;
    [SerializeField] private GameObject[] spatula;
    [HideInInspector] public bool attacking;
    private bool stop;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpatulaSwipe()
    {
        attacking = true;
        Vector3 currentpos = spatula[0].transform.localPosition;
       
         sword[0].SetTrigger("Swing");
         sword[1].SetTrigger("Swing");
         spatula[1].transform.position = spatula[2].transform.position;
         spatula[1].transform.eulerAngles =  spatula[3].transform.eulerAngles;
         
        
        yield return new WaitForSeconds(.35f);
        spatula[0].transform.localPosition = currentpos;
       
        attacking = false;
    }
}
