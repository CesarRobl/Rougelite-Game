using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private TempPlayer temp;
    public GameObject spatula;

    [SerializeField] private Sprite[] chardir;
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        temp = GetComponentInParent<TempPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpriteDir();
    }
    
    // changes the player sprite based on the direciton its looking at using its z rotation
    void ChangeSpriteDir()
    {

        if (GMController.gm.holder.eulerAngles.z >= 180)
        {
            sr.sprite = chardir[0];
            //spatula.transform.position = new Vector3(spatula.transform.position.x, spatula.transform.position.y, -.5f);
        }
        else if (GMController.gm.holder.eulerAngles.z < 180)
        {
            sr.sprite = chardir[1];
            //spatula.transform.position = new Vector3(spatula.transform.position.x, spatula.transform.position.y, 0);
        }
    }
}
