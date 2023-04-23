using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCat : MonoBehaviour
{
    Collider Box_Collider;
    private Animator Box_anim;
    int times = 0;
    public GameObject Cat;

    public static int HPbottle;

    float boxTime;

    public GameObject C;

    // Start is called before the first frame update
    void Start()
    {
        Box_Collider = GetComponent<BoxCollider>();
        Box_Collider.enabled = true;
        Box_anim = GetComponent<Animator>();
        Cat.SetActive(false);
        C.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void OnTriggerStay(Collider collision)  //是否在箱子旁
    {
        if (collision.tag == "Player")
        {
            C.SetActive(true);
            boxTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.C) && times < 1)
            {
                HPbottle++;

                boxTime = 0;
                times = 1;
                Box_anim.SetTrigger("In_Box");
                
            }
            if (boxTime > 2.9f && times == 1)
            {
                C.SetActive(false);
                Cat.SetActive(true);
                times = 2;
            }
            if(boxTime >6.9f && times == 2)
            {
                C.SetActive(false);
                Cat.SetActive(false);
                Box_Collider.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            C.SetActive(false);
        }

    }

}
