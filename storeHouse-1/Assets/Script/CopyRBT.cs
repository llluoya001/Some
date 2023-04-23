using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRBT : MonoBehaviour
{
    Rigidbody CleanRBT_Rigid;
    private Animator ani;
    Transform _Transform;
    Transform face_player;
    float _face;

    //public GameObject atkarea;

    float exATK;

    public GameObject player;
    public float speed;
    public bool faceLeft = true;
    float distance;

    bool _attack;
    float stoptime;
    float attack;
    int attackTime;
    int copy;

    public GameObject ATK;
    public Transform attackpoint;

    void Awake()
    {
        player = GameObject.Find("Player2");
    }

    void Start()
    {

        CleanRBT_Rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        _Transform = GetComponent<Transform>();//抓位置

        _attack = false;
        //atkarea.SetActive(false);

        PlayerContorl._jumpz = false;

        float _x = PlayerContorl.x;

        attackTime = 1;

    }

    // Update is called once per frame
    void Update()
    {
        _face = player.transform.position.x;
        distance = Vector3.Distance(transform.position, player.transform.position);

        filp();
        ATTACK();
        _attack = false;


    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "ATK")
        {
            ani.SetTrigger("Hurt");
            _attack = true;
            
        }
        else
            _attack = false;
    }

    void ATTACK()
    {
        exATK += Time.deltaTime;

        if (exATK == 30)
        {
            copy = 1;
            ani.SetBool("walk", false);
            ani.SetTrigger("Attack");
            if(copy == 1)
            {
                Instantiate(ATK, attackpoint.position, attackpoint.rotation);
                copy = 0;
            }
            exATK = 0;
        }

        if (distance > 5 && _attack == false)
        {

            Vector3 nextpos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            _Transform.position = Vector3.Lerp(transform.position, nextpos, 0.001f);
            ani.SetBool("walk", true);
        }

        attack += Time.deltaTime;

        if (distance <= 4 && _attack == false)
        {
            if (attackTime == 1)
            {
                ani.SetBool("walk", false);
                attack = 0;
                attackTime = 2;
            }

            if (attack > 0.1f && attack < 0.3f)
            {
                copy = 1;
                ani.SetBool("walk", false);
                attack = 1;
                ani.SetTrigger("Attack");
                if (copy == 1)
                {
                    Instantiate(ATK, attackpoint.position, attackpoint.rotation);
                    copy = 0;
                }
                attackTime = 2;
            }
            else if (attack > 1.5)
            {
                ani.SetBool("walk", false);
                ani.SetTrigger("idle");
                //atkarea.SetActive(false);
            }

            if (attack > 2.5f)
            {
                ani.SetBool("walk", false);
                //atkarea.SetActive(false);
                attackTime = 1;

            }
        }
    }

    void filp() //攻擊面向玩家
    {
        if (distance <= 10)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                faceLeft = false;
            }
            else if (transform.position.x > _face)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                faceLeft = true;
            }
        }
    }
}
