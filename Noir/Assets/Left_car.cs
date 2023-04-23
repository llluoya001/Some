using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_car : MonoBehaviour
{
    private AudioSource myAudioSource;
    public GameObject one, two; // 怪物
    private Animator Enemy_anim;
    float x;


    // Start is called before the first frame update
    void Start()
    {
        Enemy_anim = GetComponent<Animator>();
        one.SetActive(false);
        two.SetActive(false);
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manergent.Third)
        {
            x += Time.deltaTime;
            Enemy_anim.Play("Carmove");

            if (x>1)
            {
                one.SetActive(true);
                if (x > 1.5f)
                {
                    two.SetActive(true);
                }
                
            }

        }
    }

}

