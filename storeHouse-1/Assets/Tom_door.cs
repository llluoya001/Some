using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tom_door : MonoBehaviour
{
    Collider Door_Collider;

    // Start is called before the first frame update
    void Start()
    {
        Door_Collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)  //ê•î€ç›ínñ 
    {

        if (collision.tag == "ATK")
        {

        }

    }
}
