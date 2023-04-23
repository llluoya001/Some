using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terebodo : MonoBehaviour
{
    public Transform Up, Down;
    public GameObject m_Player,Q1,Q2;
    bool First,Sec,canUse;
    int n;
    float x;

    // Start is called before the first frame update
    void Start()
    {

        First = true;
        Sec = false;
        n = 0;

        Q1.SetActive(false);
        Q2.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        

        if (x < 0)
        {
            n = 0;
            PlayerContorl.TLplaying = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && canUse)
        {
            PlayerContorl.TLplaying = true;
            canUse = false;


            if (First && n == 0)
            {
                Sec = true;
                First = false;
                canUse = false;
                m_Player.transform.position = new Vector3(Up.transform.position.x, Up.transform.position.y, Up.transform.position.z);
                PlayerContorl.TLplaying = false;
                x = 1f;
                n = 1;
                Q1.SetActive(false);
            }

            if (Sec && n == 0)
            {
                First = true;
                Sec = false;
                canUse = false;
                m_Player.transform.position = new Vector3(Down.transform.position.x, Down.transform.position.y, Down.transform.position.z);
                PlayerContorl.TLplaying = false;
                x = 1f;
                n = 1;
                Q2.SetActive(false);
            }

        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Down")
        {
            n = 0;
            Sec = false;
            First = true;
            canUse = false;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            canUse = true;
            x -= Time.deltaTime;
            if (First)
            {
                Q1.SetActive(true);
            }

            if (Sec)
            {
                Q2.SetActive(true);
            }
        }

    }

    void OnTriggerExid(Collider collision)
    {
        if (collision.tag == "Player")
        {
            canUse = false;

        }

    }
}
