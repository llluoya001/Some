using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Collider Ground_Collider;
    float Jump_timer;
    

    // Start is called before the first frame update
    void Start()
    {
        Ground_Collider = GetComponent<BoxCollider>();
        Ground_Collider.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump_timer += Time.deltaTime;
        if (Jump_timer > 0.1f)
        {
            Ground_Collider.enabled = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && PlayerContorl.isJump == false)
        {

            Ground_Collider.enabled = false;
            Jump_timer = 0f;

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "player")
        {
            PlayerContorl.isJump = true;

        }
    }
}
