using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator Enemy_anim;
    float x;
    bool Y;
    public Transform target;
    int speed = 2;
    public GameObject atk;

    public float groundDetectLength;
    Rigidbody Enemy_Rigid;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_Rigid = GetComponent<Rigidbody>();
        Enemy_anim = GetComponent<Animator>();
        atk.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        x += Time.deltaTime;

        if (ManerG.LeftUp)
        {
            if (x > 3)
            {
                var step = speed * Time.deltaTime;
                target.position = new Vector3(0.361f, transform.position.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                Enemy_anim.Play("BG_walk");
                atk.SetActive(true);

            }
            if (x > 4)
            {
                Destroy(this.gameObject);
            }
        }
        


    }

    

}
