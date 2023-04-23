using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public GameObject ATK,slip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerContorl.Alive)
        {
            ATK.SetActive(true);
            slip.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            {

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    ATK.SetActive(false);
                    slip.SetActive(true);
                }
                else
                {
                    slip.SetActive(false);
                }

            }
            else
            {
                ATK.SetActive(true);
                slip.SetActive(false);
            }

        }
    }
}
