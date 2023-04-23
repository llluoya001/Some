using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRB : MonoBehaviour
{
    Collider Ground_Collider;
    // Start is called before the first frame update
    void Start()
    {
        Ground_Collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider collision)  //ê•î€ç›ínñ 
    {
        if (collision.tag == "Player")
        {
            Ground_Collider.enabled = false;
        }
    }
}

