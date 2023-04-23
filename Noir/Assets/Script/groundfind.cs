using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundfind : MonoBehaviour
{
    bool isJump,Jumping,isGround;
    public GameObject D_eff;
    public GameObject J_eff;
    public Transform attackpoint;

    float Down_timer;


    // Start is called before the first frame update
    void Start()
    {
        isJump = false;
        Jumping = false;
        isGround = false;

        //D_eff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!isGround)
            {
                Jumping = false;
            }
            else
            {
                Jumping = true;
                isGround = false;
            }

            if (Jumping)
            {
                Instantiate(J_eff, attackpoint.position, attackpoint.rotation);
                Jumping = false;
            }

            isJump = true;
        }

    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "Ground" && isJump == true)
        {
            isGround = true; 
            Jumping = false; 
            isJump = false;
            Instantiate(D_eff, attackpoint.position, attackpoint.rotation);

        }
    }

}
