using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfDown : MonoBehaviour
{
    public BoxCollider try1,try2;
    float x,y;
    bool Tou;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        x -= Time.deltaTime;
        y -= Time.deltaTime;

        if (PlayerContorl.isClaming)
        {
            
            try1.enabled = false;
            try2.enabled = false;
            x = 0.5f;
            y = 0.1f;
        }
        else if (Tou && PlayerContorl.isClaming == false)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                try1.enabled = false;
                try2.enabled = false;
                x = 0.5f;
                y = 0.1f;
            }
        }
        else
        {
            if (x < 0 && PlayerContorl.isClaming == false && !Tou)
            {
                try1.enabled = true;
                try2.enabled = true;
            }

            if (y < 0 && !Tou && PlayerContorl.isClaming == false)
            {
                try1.enabled = true;
                try2.enabled = true;
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Tou = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Tou = false;
        }
    }



}
