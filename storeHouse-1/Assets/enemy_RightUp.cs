using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class enemy_RightUp : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip JumpAudio, ATKAudio;
    public GameObject target, DieBlood;
    Transform target_P, target_E;

    public Transform bloodin;
    public GameObject Blo;

    Collider Enemy_Collider;
    private Animator Enemy_anim;
    Rigidbody Enemy_Rigid;

    public static bool key;

    public BoxCollider try1, try2;
    float x_distance, y_distance;

    bool Alive, isATK;
    float dieTime, findTime;
    int power,n;
    float _face, time_x;
    bool faceLeft,HitTrue,sec,HasPlayer;
    public GameObject ATKArea;
    float AtkCD,Beatkk;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        Enemy_Rigid = GetComponent<Rigidbody>();
        Enemy_anim = GetComponent<Animator>();
        key = false;

        Enemy_Collider = GetComponent<BoxCollider>();
        Enemy_Collider.enabled = true;
        try1.enabled = true;
        try2.enabled = true;
        faceLeft = true;
        Alive = true;
        n = 1;
        ATKArea.SetActive(false);
        DieBlood.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player");
        target_P = target.transform;
        target_E = this.transform;
        HasPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        AtkCD -= Time.deltaTime;
        Beatkk -= Time.deltaTime;

        if (ManerG.RightUp || Manergent.Four)
        {
            
            if (Alive)
            {
                findTime -= Time.deltaTime;
                if (Beatkk > 0.8f && Beatkk < 1.5f)
                {
                    n = 0;
                    HitTrue = false;
                }
                if (x_distance > 6f)
                {
                    HasPlayer = false;
                    Enemy_anim.Play("idle");
                }
                else
                    HasPlayer = true;

                filp();
                _face = target_P.position.x;
                x_distance = Mathf.Abs(transform.position.x - target_P.transform.position.x);
                y_distance = Mathf.Abs(transform.position.y - target_P.transform.position.y);
                if (y_distance<0.2f && HasPlayer)
                {

                    if (x_distance > 1f && !HitTrue)
                    {
                        if (faceLeft)
                        {
                            Enemy_anim.Play("Run");
                            Enemy_Rigid.velocity = new Vector2(5.5f, Enemy_Rigid.velocity.y);
                            findTime = 0.2f;
                            AtkCD = 0.2f;
                        }
                        else if (!faceLeft)
                        {
                            Enemy_anim.Play("Run");
                            Enemy_Rigid.velocity = new Vector2(-5.5f, Enemy_Rigid.velocity.y);
                            findTime = 0.2f;
                            AtkCD = 0.2f;
                        }

                    }
                    if (x_distance <= 1f && AtkCD < 0 && !HitTrue && findTime < 0)
                    {
                        AtkCD = 3f;
                        Enemy_anim.Play("ATK");
                        ATKArea.SetActive(true);
                    }
                }
                

            }

        }

        
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.tag == "ATK")
        {
            if(n == 1)
            {
                myAudioSource.PlayOneShot(JumpAudio);
                CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, .1f, 1f);
                Enemy_anim.Play("BeATK");
                ATKArea.SetActive(false);
                Time.timeScale = 0.8f;
                HitTrue = true;
                Beatkk = 2;

                if (target_P.transform.position.x < this.gameObject.transform.position.x)
                {
                    Enemy_Rigid.velocity = new Vector3(17, 0);//之後改隨機
                    
                    findTime = 0f;
                    AtkCD = 0.2f;
                }
                else if (target_P.transform.position.x > this.gameObject.transform.position.x)
                {
                    Enemy_Rigid.velocity = new Vector3(-17,0);
                    findTime = 0f;
                    AtkCD = 0.2f;
                }
            }

            if (n == 0)
            {
                myAudioSource.PlayOneShot(JumpAudio);
                power = Random.Range(4, 7);
                CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
                Enemy_anim.Play("Die");
                isATK = true;
                Enemy_Collider.enabled = false;
                Time.timeScale = 0.8f;
                ATKArea.SetActive(false);
                Instantiate(Blo, bloodin.position, bloodin.rotation);

                if (target_P.transform.position.x < this.gameObject.transform.position.x)
                {
                    Enemy_Rigid.velocity = new Vector3(power, 7);//之後改隨機
                    Alive = false;
                    key = true;
                    DieBlood.SetActive(true);
                }
                else if (target_P.transform.position.x > this.gameObject.transform.position.x)
                {
                    Enemy_Rigid.velocity = new Vector3(-power, 7);
                    Alive = false;
                    key = true;
                    DieBlood.SetActive(true);
                }
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

    void filp() //攻擊面向玩家
    {
        if (x_distance <= 6)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
                faceLeft = true;
            }
            else if (transform.position.x > _face)
            {
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                faceLeft = false;
            }
        }
    }
}
