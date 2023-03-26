using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour

{
    public GameObject Player;
    public TextMeshProUGUI[] TextMovement;

    public GameObject Attack;

    public int TutInt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Movements()
    {
        SpriteRenderer sprite = Player.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(1);
        
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        
    }
}
