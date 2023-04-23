using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_botton : MonoBehaviour
{
    public GameObject S_down;
    bool intohole;
    bool RBin;
    float Go_timer;

    // Start is called before the first frame update
    void Start()
    {
        S_down.SetActive(true);
        intohole = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerContorl.isClaming == true)
        {
            S_down.SetActive(false);
        }

        if (intohole && !RBin)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
 
                S_down.SetActive(false);
                intohole = false;
            }
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Head")
        {
            S_down.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            intohole = true;
        }

        if (collision.tag == "Enemy")
        {
            RBin = true;
        }


    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Head")
        {
            S_down.SetActive(true);
        }

        if (collision.tag == "Player")
        {

            intohole = false;
        }

        if (collision.tag == "Enemy")
        {
            RBin = false;
        }

    }
}
