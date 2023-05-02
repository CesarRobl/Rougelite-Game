using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private TempPlayer temp;
    public GameObject spatula;

    [SerializeField] private Sprite[] chardir;
    [SerializeField] private Sprite[] attackDir;
    private int num;
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        temp = GetComponentInParent<TempPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GMController.gm.ani.attacking)ChangeSpriteDir();
        else sr.sprite = attackDir[num];
    }
    
    // changes the player sprite based on the direction its looking at using its z rotation
    void ChangeSpriteDir()
    {

        if (GMController.gm.holder.eulerAngles.z >225 && GMController.gm.holder.eulerAngles.z<=315)
        {
            num = 0;
        }
        
        else if (GMController.gm.holder.eulerAngles.z >45&&GMController.gm.holder.eulerAngles.z<=135)
        {
            num = 1;
        }
        
        else if (GMController.gm.holder.eulerAngles.z <= 45 || GMController.gm.holder.eulerAngles.z > 315)
        {
            
            num = 2;
        }
        else if (GMController.gm.holder.eulerAngles.z > 135 && GMController.gm.holder.eulerAngles.z <= 225)
        {
           
            num = 3;
        }

        sr.sprite = chardir[num];
    }

    
}
