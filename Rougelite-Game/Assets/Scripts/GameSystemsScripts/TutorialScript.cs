using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour

{
    public GameObject Player;
    public TextMeshProUGUI[] TextMovement;

    private Color OG;
    public GameObject Attack;

    public int TutInt = 0;
    // Start is called before the first frame update
    void Start()
    {
        OG = Player.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutInt == 1) StartCoroutine(FadeMovement());
        else StartCoroutine(AttackFade());

    }

    IEnumerator AttackFade()
    {
        Attack.SetActive(false);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(Movements());
    }
    IEnumerator AttackTut()
    {
        Attack.SetActive(true);
        yield return new WaitForSeconds(.1f);
        
    }

   public void ButtonPressed()
    {
        if (TutInt == 1)
        {
            TutInt = 0;
        }
        else
        {
            TutInt = 1;
        }
    }
    IEnumerator Movements()
    {
        SpriteRenderer sprite = Player.GetComponent<SpriteRenderer>();
        sprite.color = OG;
        for (int i = 0; i < TextMovement.Length; i++)
        {
            TextMovement[i].alpha = 1;
        }
        yield return new WaitUntil(() => TextMovement[0].alpha <= 1);

    }
    IEnumerator FadeMovement()
    { 
        SpriteRenderer sprite = Player.GetComponent<SpriteRenderer>();
        sprite.color -= new Color(0, 0, 0, .75f * Time.deltaTime);
        for (int i = 0; i < TextMovement.Length; i++)
        {
            TextMovement[i].alpha -= .75f*Time.deltaTime;
        }

        yield return new WaitUntil(() => TextMovement[0].alpha <= 0);
        StartCoroutine(AttackTut());
    }

    public void SkipTut()
    {
        SceneManager.LoadScene("RandomLevel");
    }
}
