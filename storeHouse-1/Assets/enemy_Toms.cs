using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class enemy_Toms : MonoBehaviour
{
    public GameObject target,DieBlood;
    public BoxCollider try1, try2;
    public bool Right;

    private AudioSource myAudioSource;
    public AudioClip JumpAudio;

    public Transform bloodin;
    public GameObject Blo;


    public Transform target_P, target_E;
    bool isATK;
    float dieTime,findTime;
    bool Alive;
    public GameObject ATKArea;

    int power,n;

    //行為模式
    float x_distance, y_distance;//拿到玩家距離
    float YD;
    int nowstate;//0是待機、1是巡邏、2是追擊攻擊
    float _face;
    bool isChangestate; //可以改變狀態
    float stoptime;//停止時間
    bool faceLeft,atktrue;//面向左
    public float leftx, rightx;//巡邏範圍
    float exATK,AtkCD;//追擊時間


    Collider Ground_Collider;
    private Animator Enemy_anim;
    Rigidbody Enemy_Rigid;

    public float groundDetectLength;
    bool HasDoor, HasPlayer,HasTrance;


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
        DieBlood.SetActive(false);

        Alive = true;
        HasTrance = false;
        n = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AtkCD -= Time.deltaTime;
        
        _face = target_P.position.x;
        x_distance = Mathf.Abs(transform.position.x - target_P.transform.position.x);
        y_distance = Mathf.Abs(transform.position.y - target_P.transform.position.y);

        if (!HasTrance)
        {
            if (!Right)
            {
                HasDoor = CheckJumpWalkLeft();
            }
            else
                HasDoor = CheckJumpWalkRight();

        }


        if (y_distance < 0.2f)
        {
            HasPlayer = true;
        }
        if(y_distance > 0.2f)
        {
            HasPlayer = false;
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
            new Ray(transform.position, Vector3.right),
            out RaycastHit hitInfo, groundDetectLength, mask))
        {
            return true;

        }
        return false;
    }

    private bool CheckJumpWalkRight()
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
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * groundDetectLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * groundDetectLength);

    }

    void distancechange()//改變距離
    {
        if (!HasDoor)
        {
            if (HasPlayer)
            {
                if (x_distance <= 6f)
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
            Enemy_anim.Play("BG_idle");

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
                Enemy_anim.Play("BG_walk");

                Enemy_Rigid.velocity = new Vector2(-2.5f, Enemy_Rigid.velocity.y);

                if (transform.position.x < leftx)
                {
                    Enemy_anim.Play("BG_idle");

                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    faceLeft = true;
                    nowstate = 0;
                }
            }
            else if (faceLeft)
            {
                Enemy_anim.Play("BG_walk");
                Enemy_Rigid.velocity = new Vector2(2.5f, Enemy_Rigid.velocity.y);

                if (transform.position.x > rightx)
                {
                    Enemy_anim.Play("BG_idle");
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    faceLeft = false;
                    nowstate = 0;
                }
            }

        }
        else if (nowstate == 2 && !HasDoor) //追擊
        {
            if (y_distance < 0.2f)
            {

                HasTrance = true;
                exATK += Time.deltaTime;
                findTime -= Time.deltaTime;
                if (n == 0)
                {
                    findTime = 0.3f;
                    Enemy_anim.Play("BG_idle");
                    n++;
                }

                if (exATK > 0.5f)
                {

                    if (x_distance > 0.8f && findTime < 0 && !atktrue)
                    {

                        if (!faceLeft)
                        {
                            AtkCD = 0.1f;
                            Enemy_anim.Play("BG_Run");
                            Enemy_Rigid.velocity = new Vector2(-5f, Enemy_Rigid.velocity.y);
                            ATKArea.SetActive(false);
                            atktrue = false;
                        }
                        else if (faceLeft)
                        {
                            AtkCD = 0.1f;
                            Enemy_anim.Play("BG_Run");
                            Enemy_Rigid.velocity = new Vector2(5f, Enemy_Rigid.velocity.y);
                            ATKArea.SetActive(false);
                            atktrue = false;
                        }
                    }

                    if (x_distance <= 0.8f && HasPlayer && AtkCD < 0)
                    {
                        atktrue = true;
                        Enemy_anim.Play("BG_Attack");
                        AtkCD = 3f;
                        ATKArea.SetActive(true);

                    }

                }
            }
            

        }

    }

    void filp() //攻擊面向玩家
    {
        if (x_distance <= 6 && !HasDoor && HasPlayer && Alive)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                faceLeft = true;
            }
            else if (transform.position.x > _face && Alive)
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
            myAudioSource.PlayOneShot(JumpAudio);
            Enemy_anim.Play("BG_die");
            isATK = true;
            Ground_Collider.enabled = false;
            ATKArea.SetActive(false);
            Instantiate(Blo, bloodin.position, bloodin.rotation);
            Time.timeScale = 0.8f;

            if (target_P.transform.position.x < this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(power, 8);//之後改隨機
                Alive = false;
                DieBlood.SetActive(true);
            }
            else if (target_P.transform.position.x > this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(-power, 8);
                Alive = false;
                DieBlood.SetActive(true);
            }

        }

        if (collision.tag == "Ground")
        {


            if (isATK == true)
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
