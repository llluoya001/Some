using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UptoStairs : MonoBehaviour
{
    static bool CanUp;
    int A;

    Rigidbody Player_Rigid;

    // Start is called before the first frame update
    void Start()
    {
        Player_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanUp)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Player_Rigid.velocity = new Vector3(0, 2);

            }
        }
    }

    void OnTriggerEnter(Collider collision)  //ê•î€ç›ínñ 
    {
        if (collision.tag == "ST_CheckPoint")
        {
            if (A == 0)
            {
                CanUp = true;
                A++;
            }

            if (A == 1)
            {
                CanUp = true;
                A = 0;
            }
            
        }

    }
}
