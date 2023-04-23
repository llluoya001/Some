using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManerG : MonoBehaviour
{
    public static bool LeftUp, RightDown, RightUp;
    public GameObject Door1,Ani,Q,Q2,Timelineee,T222;
    public BoxCollider up,OutDoor,RB;
    bool Getout,top;
    bool CanOpen, NoOpen,CanOut;
    float x;

    // Start is called before the first frame update
    void Start()
    {
        LeftUp =false;
        RightDown = false;
        RightUp = false;

        Q.SetActive(false);
        Door1.SetActive(false);
        T222.SetActive(false);
        Timelineee.SetActive(false);

        OutDoor.enabled = false;
        RB.enabled = true;
        Getout = false;
    }

    // Update is called once per frame
    void Update()
    {

        x += Time.deltaTime;

        if (!PlayerContorl.Alive)
        {
            LeftUp = false;
            RightDown = false;
            RightUp = false;
            OutDoor.enabled = false;
            RB.enabled = true;
            Getout = false;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Q.SetActive(false);
                Door1.SetActive(false);
                Timelineee.SetActive(false);
                T222.SetActive(false);
            }

        }

        if (CanOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Getout = true;
                OutDoor.enabled = true;
                RB.enabled = false;
                x = 0f;
                Timelineee.SetActive(true);

            }
            if (Getout)
            {
                if (x >1f)
                {
                    Q.SetActive(true);
                }
            }
            
        }

        if (CanOut)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                x = 0f;
                top = true;
                Ani.SetActive(true);

            }

            if (x > 2 && top)
            {
                SceneManager.LoadScene(4);
            }
        }

        if (NoOpen)
        {

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            LeftUp = true;
            Door1.SetActive(true);
            up.enabled = false;

        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (enemy_RightUp.key && !Getout)
            {
                CanOpen = true;
            }

            if (!enemy_RightUp.key && !Getout)
            {
                CanOpen = false;
                NoOpen = true;
            }

            if (Getout && x > 2)
            {
                CanOut = true;
                Q2.SetActive(true);
                T222.SetActive(true);
            }
        }
        


        

    }
}
