using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manergent : MonoBehaviour
{
    public static bool Frist, Secend, Third, Four,Five;
    public GameObject right, left;

    float x =3;

    // Start is called before the first frame update
    void Start()
    {
        Secend = false;
        Third = false;
        Four = false;
        left.SetActive(false);
        right.SetActive(false);
        run.TL = true;
        PlayerContorl.TLplaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        x -= Time.deltaTime;

        if (!PlayerContorl.Alive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                x = 1;
                Frist = false;
                Secend = false;
                Third = false;
                Four = false;
                left.SetActive(false);
                right.SetActive(false);
                PlayerContorl.TLplaying = false;
            }
            
        }

        if (PlayerContorl.Alive)
        {
            if (x < 0)
            {
                Frist = true;
                PlayerContorl.TLplaying = false;
            }

            if (Third)
            {
                left.SetActive(true);
                if (Four)
                {
                    right.SetActive(true);
                }
            }
        }

        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Third = true;
        }
    }
}
