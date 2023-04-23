using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    private Animator BG_anim;
    Rigidbody BG_Rigid;


    public Transform target_P;
    float x_distance;
    bool faceLeft,Zk,idletime,Xk;
    public float leftx, rightx,time_i,flip_time;
    Collider Ground_Collider;
    public GameObject BGY;

    // Start is called before the first frame update
    void Start()
    {
        target_P = GameObject.FindGameObjectWithTag("Player").transform;
        BG_Rigid = GetComponent<Rigidbody>();
        BG_anim = GetComponent<Animator>();
        Ground_Collider = GetComponent<BoxCollider>();
        Zk = false;
        flip_time = 1f;
        BGY.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        x_distance = (transform.position.x - target_P.transform.position.x);
        time_i -= Time.deltaTime;

        if (time_i > 0)
        {
            idletime = true;
        }
        else
            idletime = false;

        if (Zk && !idletime)
        {
            Flip();
        }
        if (idletime)
        {
            BG_anim.Play("BG_idle");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            faceLeft = true;
            
            Zk = true;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Zk = false;
            Xk = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Xk = false;
            BG_anim.Play("BG_die");
            Ground_Collider.enabled = false;
            BG_Rigid.useGravity = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Destroy(this.gameObject);
            BGY.SetActive(true);
        }



        if (Xk)
        {
            Run();
        }

    }

    void Flip()
    {
        if (!faceLeft)
        {
            flip_time -= Time.deltaTime;
            if (flip_time >0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (flip_time < 0f)
            {
                BG_Rigid.velocity = new Vector2(2.5f, BG_Rigid.velocity.y);
                BG_anim.Play("BG_walk");
            }

            if (transform.position.x > rightx)
            {
                faceLeft = true;
                time_i = 2f;
                idletime = true;
                flip_time = 0.5f;

            }

        }
        else if (faceLeft)
        {
            flip_time -= Time.deltaTime;
            if (flip_time > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (flip_time < 0f)
            {
                BG_Rigid.velocity = new Vector2(-2.5f, BG_Rigid.velocity.y);
                BG_anim.Play("BG_walk");
            }
            /*transform.localScale = new Vector3(1f, 1f, 1f);
            BG_Rigid.velocity = new Vector2(-2.5f, BG_Rigid.velocity.y);
            BG_anim.Play("BG_walk");*/

            if (transform.position.x < leftx)
            {
                faceLeft = false;
                time_i = 2f;
                idletime = true;
                flip_time = 0.5f;
            }
        }
    }
    void Run()
    {
        if (Mathf.Abs(x_distance) > 0.8f)
        {
            BG_Rigid.velocity = new Vector2(-3.5f, BG_Rigid.velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
            BG_anim.Play("BG_Run");
        }

        if (Mathf.Abs(x_distance) < 0.8f)
        {
            BG_anim.Play("BG_Attack");
        }
    }
}
