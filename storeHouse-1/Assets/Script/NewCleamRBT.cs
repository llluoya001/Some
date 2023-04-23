using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCleamRBT : MonoBehaviour
{
    Rigidbody CleanRBT_Rigid;
    private Animator ani;
    Transform _Transform;
    public Transform face_player;
    float _face;

    //傷害
    public static float dmg;
    float _x;
    public GameObject atkarea;

    float exATK;

    
    public GameObject player;
    float distance;//與玩家距離
    int nowstate;//所處狀態
    public Transform pos_left, pos_right;
    public float speed;
    public bool faceLeft = true;
    public float leftx, rightx;

    bool _attack;

    bool isRandom;
    bool isChangestate;
    float stoptime;
    float attack;
    int attackTime;

    //攻擊範圍
    public GameObject ATK;
    public Transform attackpoint;

    // Start is called before the first frame update
    void Start()
    {
        CleanRBT_Rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        _Transform = GetComponent<Transform>();//抓位置

        _attack = false;
        atkarea.SetActive(false);

        
        leftx = pos_left.position.x;
        rightx = pos_right.position.x;

        //已經拿到座標了，刪了它
        Destroy(pos_left.gameObject);
        Destroy(pos_right.gameObject);

        nowstate = 0; //0是待機、1是巡邏、2是追擊攻擊
        isRandom = true;
        isChangestate = true;

        PlayerContorl._jumpz = false;

        float _x = PlayerContorl.x;

        attackTime = 1;

    }

    // Update is called once per frame
    void Update()
    {
        _face = face_player.position.x;
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 nextpos = new Vector3(face_player.position.x, face_player.position.y, face_player.position.z);

        
        distancechange(); //改變距離
        statechange(); //改變狀態
        filp();
        _attack = false;


        //_Transform.position = Vector3.Lerp(transform.position, nextpos, fractionOfJourney);

    }

    void fixedupdate()
    {
        
    }

    void distancechange()
    {
        if(distance <= 10)
        {
            
            isChangestate = true; //改變狀態
            nowstate = 2; //追擊、攻擊
        }
        else if(distance > 10)
        {
            if (isChangestate == true)//如果可以改變狀態
            {
                nowstate = 0;
                stoptime = 0;
                isChangestate = false;
            }
        }
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

    void statechange()
    {
        if (nowstate == 0) //待機狀態
        {
            attackTime = 1;
            ani.SetBool("walk", false);
            stoptime += Time.deltaTime;
            _attack = false;

            if (stoptime > 3)
            {
                isRandom = true;
                nowstate = 1;
                stoptime = 0;
                _attack = false;
            }
        }
        else if (nowstate == 1) //巡邏
        {
            attackTime = 1;
            _attack = false;
            if (isRandom == true)
            {
                if (faceLeft == true)
                {
                    ani.SetBool("walk", true);
                    CleanRBT_Rigid.velocity = new Vector2(-speed, CleanRBT_Rigid.velocity.y);

                    if (transform.position.x < leftx)
                    {
                        ani.SetBool("walk", false);
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        faceLeft = false;
                        isRandom = false;
                        nowstate = 0;
                    }

                }
                else if (faceLeft == false )
                {
                    
                    ani.SetBool("walk", true);
                    CleanRBT_Rigid.velocity = new Vector2(speed, CleanRBT_Rigid.velocity.y);

                    if (transform.position.x > rightx)
                    {
                        ani.SetBool("walk", false);
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        faceLeft = true;
                        isRandom = false;
                        nowstate = 0;
                    }
                }

            }
        }
        else if (nowstate == 2 ) //追擊
        {
            
            exATK += Time.deltaTime;

            if(exATK == 15)
            {
                ani.SetBool("walk", false);
                ani.SetTrigger("Attack");
                atkarea.SetActive(true);
                exATK = 0;
            }

            if (distance > 5 && _attack == false)
            {
                
                Vector3 nextpos = new Vector3(face_player.position.x, transform.position.y, transform.position.z);
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
                    ani.SetBool("walk", false);
                    attack = 1;
                    ani.SetTrigger("Attack");
                    atkarea.SetActive(true);
                    attackTime = 2;
                }
                else if (attack > 1.5)
                {
                    ani.SetBool("walk", false);
                    ani.SetTrigger("idle");
                    atkarea.SetActive(false);
                }

                if (attack > 2.5f)
                {
                    ani.SetBool("walk", false);
                    attack = 0;
                    nowstate = 0;
                    isChangestate = true;
                    atkarea.SetActive(false);
                    attackTime = 1;

                }
            }

            

        }

    }

    void filp() //攻擊面向玩家
    {
        if (distance <= 10)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                faceLeft = false;
            }
            else if (transform.position.x > _face)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                faceLeft = true;
            }
        }
    }
}
