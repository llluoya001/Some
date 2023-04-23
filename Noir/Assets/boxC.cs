using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class boxC : MonoBehaviour
{
    Collider Ground_Collider;
    float ATK_timer;

    public static bool N;


    // Start is called before the first frame update
    void Start()
    {
        Ground_Collider = GetComponent<BoxCollider>();
        Ground_Collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ATK_timer += Time.deltaTime;

        if (ATK_timer > 0.02f)
        {
            N = false;
            if (ATK_timer > 0.05f)
            {
                Ground_Collider.enabled = true;
                
            }
        }
        

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            ATK_timer = 0;
            Ground_Collider.enabled = false;
            N = true;
        }
    }

}
