using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class enemy_Blue : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip JumpAudio;
    public GameObject target,DieBlood;
    Transform target_P, target_E;

    public Transform bloodin;
    public GameObject Blo;

    Collider Enemy_Collider;
    private Animator Enemy_anim;
    Rigidbody Enemy_Rigid;

    public BoxCollider try1, try2;
    float x_distance, y_distance;
    public Transform ATKpoint;

    bool Alive, isATK;
    float dieTime;
    float AtkCD;
    int power;
    float _face, time_x;
    bool faceLeft;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        Enemy_Rigid = GetComponent<Rigidbody>();
        Enemy_anim = GetComponent<Animator>();

        Enemy_Collider = GetComponent<BoxCollider>();
        Enemy_Collider.enabled = true;
        try1.enabled = true;
        try2.enabled = true;
        faceLeft = true;
        Alive = true;
        DieBlood.SetActive(false);


        target = GameObject.FindGameObjectWithTag("Player");
        target_P = target.transform;
        target_E = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Third._TH)
        {

            if (Alive)
            {
                filp();
                _face = target_P.position.x;
                AtkCD -= Time.deltaTime;
                if (x_distance > 3f)
                {
                    
                    if (faceLeft)
                    {
                        AtkCD = 0.2f;
                        Enemy_anim.Play("BGY_Run");
                        Enemy_Rigid.velocity = new Vector2(-5f, Enemy_Rigid.velocity.y);
                        
                    }
                    else if (!faceLeft)
                    {
                        AtkCD = 0.2f;
                        Enemy_anim.Play("BGY_Run");
                        Enemy_Rigid.velocity = new Vector2(5f, Enemy_Rigid.velocity.y);
                        
                    }

                }
                if (x_distance <= 3f && AtkCD < 0)
                {
                    AtkCD = 3f;
                    Enemy_anim.Play("BYG_attack");
                    Instantiate(bullet, ATKpoint.position, ATKpoint.rotation);
                }

            }

        }

        x_distance = Mathf.Abs(transform.position.x - target_P.transform.position.x);
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "ATK")
        {
            myAudioSource.PlayOneShot(JumpAudio);
            power = Random.Range(4, 7);
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
            Enemy_anim.Play("BGY_Die");
            isATK = true;
            Enemy_Collider.enabled = false;
            Time.timeScale = 0.8f;
            Instantiate(Blo, bloodin.position, bloodin.rotation);

            if (target_P.transform.position.x < this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(power, 7);//之後改隨機
                Alive = false;
                DieBlood.SetActive(true);
            }
            else if (target_P.transform.position.x > this.gameObject.transform.position.x)
            {
                Enemy_Rigid.velocity = new Vector3(-power, 7);
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
    void filp() //攻擊面向玩家
    {
        if (x_distance < 6)
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
