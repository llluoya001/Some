using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pro : MonoBehaviour
{
    public Transform target;
    public GameObject m_Player;
    public GameObject Player;
    public GameObject Ani;

    bool jump;

    float y,x;
    int n = 0;

    bool Stoping;

    // Start is called before the first frame update
    void Start()
    {
        Stoping = false;
        jump = false;
        Player.transform.GetComponent<PlayerContorl>().enabled = true;
        Ani.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime;
        x -= Time.deltaTime;

        if (y > 3 && Stoping)
        {
            jump = true;
            Jump();
        }


    }
    void Jump()
    {
        if (jump && n<1)
        {
            m_Player.transform.position = new Vector3(target.transform.position.x,
        target.transform.position.y, target.transform.position.z);

            PlayerContorl.TLplaying = false;
            n++;
            jump = false;
            Ani.SetActive(false);
        }
        
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Stoping = true;
            Ani.SetActive(true);
            y = 0f;
            x = 0.5f;
            if (x < 0)
            {
                PlayerContorl.TLplaying = true;
            }
        }

        

    }
}
