using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class run : MonoBehaviour
{
    public GameObject Run_eff;
    public GameObject Run_eff2;
    float run_timer = 0f;
    public Transform attackpoint;
    public static bool GD,TL;



    // Start is called before the first frame update
    void Start()
    {
        GD = true;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        if (!TL)
        {
            if (Input.GetKeyDown(KeyCode.D) && GD == true)
            {

                Instantiate(Run_eff2, attackpoint.position, attackpoint.rotation);

            }
            else if (Input.GetKeyDown(KeyCode.A) && GD == true)
            {

                Instantiate(Run_eff, attackpoint.position, attackpoint.rotation);

            }
        }
        
    }

    void OnTriggerEnter(Collider collision)  //ê•î€ç›ínñ 
    {
        if (collision.tag == "Ground")
        {
            GD = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Ground")
        {
            GD = false;
        }
    }
}

