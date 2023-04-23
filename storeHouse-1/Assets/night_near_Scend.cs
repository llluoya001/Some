using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class night_near_Scend : MonoBehaviour
{
    public GameObject target, DieBlood;
    Transform target_P, target_E;

    public Transform bloodin;
    public GameObject Blo;

    Collider Enemy_Collider;
    private Animator Enemy_anim;
    Rigidbody Enemy_Rigid;

    public BoxCollider try1, try2;
    float x_distance, y_distance;
    public GameObject ATKArea;

    bool Alive, isATK;
    float AtkCD;
    float dieTime, findTime;
    int power;
    float _face, time_x;
    bool faceLeft;

    // Start is called before the first frame update
    void Start()
    {
        Enemy_Rigid = GetComponent<Rigidbody>();
        Enemy_anim = GetComponent<Animator>();

        Enemy_Collider = GetComponent<BoxCollider>();
        Enemy_Collider.enabled = true;
        try1.enabled = true;
        try2.enabled = true;
        faceLeft = true;
        Alive = true;
        ATKArea.SetActive(false);
        DieBlood.SetActive(false);
        target = GameObject.FindGameObjectWithTag("Player");
        target_P = target.transform;
        target_E = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        AtkCD -= Time.deltaTime;

        if (Manergent.Secend)
        {
            
            _face = target_P.position.x;

            if (transform.position.z == 1)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                Enemy_anim.Play("BG_walk");
            }

            if (Alive && transform.position.z == 0)
            {
                findTime -= Time.deltaTime;
                time_x += Time.deltaTime;

                if (x_distance > 1f && time_x > 1)
                {
                    filp();
                    Enemy_anim.Play("BG_Run");

                    if (faceLeft)
                    {
                        Enemy_anim.Play("Run");
                        Enemy_Rigid.velocity = new Vector2(5f, Enemy_Rigid.velocity.y);
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                        findTime = 0.2f;
                    }
                    else if (!faceLeft)
                    {
                        Enemy_anim.Play("Run");
                        Enemy_Rigid.velocity = new Vector2(-5f, Enemy_Rigid.velocity.y);
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        findTime = 0.2f;
                    }

                }
                if (x_distance <= 1f && AtkCD < 0 && findTime < 0)
                {
                    filp();
                    AtkCD = 3f;
                    Enemy_anim.Play("BG_Attack");
                    ATKArea.SetActive(true);
                }

            }

        }

        x_distance = Mathf.Abs(transform.position.x - target_P.transform.position.x);
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "ATK")
        {
            power = Random.Range(4, 7);
            CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, 1f);
            Enemy_anim.Play("BG_die");
            Third._TH = true;
            isATK = true;
            Enemy_Collider.enabled = false;
            Time.timeScale = 0.8f;
            ATKArea.SetActive(false);
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
        if (x_distance > 0)
        {
            if (transform.position.x < _face)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                faceLeft = true;
            }
            else if (transform.position.x > _face)
            {
                faceLeft = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

    }
}
