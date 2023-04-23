using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class enemy : MonoBehaviour
{
    public GameObject target;
    public BoxCollider try1, try2;

    private AudioSource myAudioSource;
    public AudioClip DieAudio, ATKAudio;

    public Transform target_P, target_E;
    bool isATK;
    float dieTime, findTime;
    bool Alive;
    public GameObject ATKArea;

    int power;

    //行為模式
    float x_distance, y_distance;//拿到玩家距離
    float YD;
    int nowstate;//0是待機、1是巡邏、2是追擊攻擊
    float _face;
    bool isChangestate; //可以改變狀態
    float stoptime;//停止時間
    bool faceLeft;//面向左
    public float leftx, rightx;//巡邏範圍
    float exATK, AtkCD;//追擊時間


    Collider Ground_Collider;
    private Animator Enemy_anim;
    Rigidbody Enemy_Rigid;

    public float groundDetectLength;
    bool HasDoor,HasPlayer;


    // Start is called before the first frame update
    void Start()
    {
        isATK = false;
        myAudioSource = GetComponent<AudioSource>();
        Enemy_Rigid = GetComponent<Rigidbody>();
        Enemy_anim = GetComponent<Animator>();
        Ground_Collider = GetComponent<BoxCollider>();
        Ground_Collider.enabled = true;
        try1.enabled = true;
        try2.enabled = true;
        target = GameObject.FindGameObjectWithTag("Player");
        target_P = target.transform;
        target_E = this.transform;

        nowstate = 0;

        isChangestate = true;
        faceLeft = false;
        ATKArea.SetActive(false);
        Alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        AtkCD -= Time.deltaTime;
        _face = target_P.position.x;
        x_distance = Mathf.Abs(transform.position.x - target_P.transform.position.x);
        y_distance = Mathf.Abs(transform.position.y - target_P.transform.position.y);

        HasDoor = CheckJumpWalkLeft();

        if (y_distance < 0.2f)
        {
            HasPlayer = true;
        }

        if (Alive)
        {
            distancechange(); //改變距離
            statechange(); //改變狀態
            filp();
        }

    }
    //偵測撞門
    private bool CheckJumpWalkLeft()
    {
        LayerMask mask = LayerMask.GetMask("Door");

        if (Physics.Raycast(
            new Ray(transform.position, Vector3.left),
            out RaycastHit hitInfo, groundDetectLength, mask))
        {
            return true;

        }
        return false;
    }


    private void OnDrawGizmos() //可看見偵測的線
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * groundDetectLength);

    }

    void distancechange()//改變距離
    {
        if (!HasDoor)
        {
            if (HasPlayer)
            {
                if (x_distance <= 6)
                {
                    isChangestate = true; //改變狀態
                    nowstate = 2; //追擊、攻擊
                }
            }
            
        }
        else if (HasDoor)
        {
            if (isChangestate)//改變狀態
            {
                nowstate = 0;
                stoptime = 0;
                isChangestate = false;
            }
        }

    }

    void statechange()
    {
        if (nowstate == 0) //待機狀態
        {
            Enemy_anim.SetBool("walk", false);
            stoptime += Time.deltaTime;

            if (stoptime > 2)
            {
                nowstate = 1;//待機2秒變巡邏
                stoptime = 0;
            }
        }
        else if (nowstate == 1) //巡邏
        {
            if (!faceLeft)
            {
                Enemy_anim.SetBool("walk", true);
                Enemy_Rigid.velocity = new Vector2(-2.5f, Enemy_Rigid.velocity.y);

                if (transform.position.x < leftx)
                {
                    Enemy_anim.SetBool("walk", false);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    faceLeft = true;
                    nowstate = 0;
                }
            }
            else if (faceLeft)
            {
                Enemy_anim.SetBool("walk", true);
                Enemy_Rigid.velocity = new Vector2(2.5f, Enemy_Rigid.velocity.y);

                if (transform.position.x > rightx)
                {
                    Enemy_anim.SetBool("walk", false);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    faceLeft = false;
                    nowstate = 0;
                }
            }

        }
        else if (nowstate == 2) //追擊
        {
            exATK += Time.deltaTime;
            findTime -= Time.deltaTime;
            if (exATK > 0.6f)
            {
                if (x_distance > 1f )
                {
                    Enemy_anim.SetBool("walk", true);

                    if (!faceLeft)
                    {
                        Enemy_anim.SetBool("walk", true);
                        Enemy_Rigid.velocity = new Vector2(-4f, Enemy_Rigid.velocity.y);
                        findTime = 0.2f;
                        AtkCD = 0.2f;
                    }
                    else if (faceLeft)
                    {
                        Enemy_anim.SetBool("walk", true);
                        Enemy_Rigid.velocity = new Vector2(4f, Enemy_Rigid.velocity.y);
                        findTime = 0.2f;
                        AtkCD = 0.2f;
                    }
                }

                if (x_distance <= 1f &&  HasPlayer && findTime < 0 && AtkCD < 0)
                {
                    myAudioSource.PlayOneShot(ATKAudio);
                    AtkCD = 2f;
                    Enemy_anim.Play("E_ATK");
                    ATKArea.SetActive(true);
                }

            }

        }

    }

    void filp() //攻擊面向玩家
    {
        if (x_distance <= 6 && !HasDoor && HasPlayer)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                faceLeft = true;
            }
            else if (transform.position.x > _face)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                faceLeft = false;
            }
        }
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "Player")
        {
            

        }

        if (collision.tag == "ATK")
        {
            power = Random.Range(4, 7);
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
            myAudioSource.PlayOneShot(DieAudio);
            Enemy_anim.SetTrigger("atk");
            isATK = true;
            Ground_Collider.enabled = false;
            ATKArea.SetActive(false);
            Time.timeScale = 0.8f;

            if (target_P.transform.position.x < this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(power, 10);//之後改隨機
                Alive = false;
            }
            else if (target_P.transform.position.x > this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(-power, 10);
                Alive = false;
            }
            
        }

        if(collision.tag == "Ground")
        {

            
            if (isATK==true)
            {
                dieTime = 0;
                dieTime += Time.deltaTime;
                Enemy_Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                try1.enabled = false;
                
                if (dieTime < 0.1f)
                {
                    try2.enabled = false;
                    Time.timeScale = 1f;
                }
            }
            

        }

    }
}
