using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerContorl : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip JumpAudio,ATKAudio,dieAudio;
    public CapsuleCollider try1;
    public BoxCollider try2;

    public GameObject DieAin;

    public static bool Alive;

    private Animator Player_anim;
    private float ComboCount = 0f;
    public static bool _jumpz;
    bool Ztimes,Plus,isST,ForD;
    public Transform target;

    public static bool canClam , isClaming , isGround; //可以抓牆、正在抓牆、在地上

    Collider player_Collider;

    public static Vector3 player_transform;
    public static bool TLplaying = false;

    //Dash
    private bool canDash;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 0.8f;

    //轉身
    float inputHorizontal;
    float inputVertical;

    //換牆時禁止動作
    public float WallJumpTime = 0.2f;
    private float wallJumpCounter;

    public int speed;
    public int Jump;

    public enum Face { Right,Left};
    public Face face;

    public static float x;

    bool isAtk,Sting,atk2;
    int t;
    int moveFace;
    public static bool TimeLine;

    //是否在空中
    public static bool isJump,tt, ttng;

    //第一擊跟第二擊時間差
    float ATK_timer = 0f;
    float ATK_timerCount = 0.5f;

    //睡覺用時間計時器
    float Sleep_timer = 1f;
    float walk_timer = 0;
    float Jumping_timer;
    float Claming_timer = 1f;

    //鉤鎖用計時器
    float Go_timer;
    float Clam_timer;

    bool walking; //正在走路
    bool Nwalk; //禁止走路
    
    float walktime;
    float Jump_timer;

    public float A, B,Diet;
    float Y_timer = 0.1f;

    public float FallingSpeed;
    public bool fallingCheck = false;
    public bool jumptofallcheck = false;

    bool Downl;
    int An,Cb;

    //停止案A的時候的計時器
    float Aplus;

    //射線需求
    public float groundDetectLength;

    public Transform Creatpoint;
    public GameObject Creat, Wrong;
    GameObject Del;

    Rigidbody Player_Rigid;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        player_transform = this.transform.position;
        _jumpz = false;
        Player_Rigid = GetComponent<Rigidbody>();
        Player_anim = GetComponent<Animator>();
        isJump = false;
        Nwalk = false;
        TimeLine = false;
        canClam = false;
        canDash = true;
        t = 0;

        Alive = true;


    }

    // Update is called once per frame
    void Update()
    {
       Diet += Time.deltaTime;

        if (!Alive && Diet>0.4f) //ㄏㄏ重生
        {
            Del = GameObject.FindGameObjectWithTag("Creat");
            Time.timeScale = 0f;
            Wrong.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ttng = true;
                Diet = 1f;
            }
            if (ttng)
            {
                DieAin.SetActive(true);
                Wrong.SetActive(false);
                TLplaying = true;
                Player_Rigid.useGravity = true;
                try1.enabled = true;
                try2.enabled = true;
                Time.timeScale = 1f;
            }
            if (Diet > 1.5f && ttng)
            {
                DieAin.SetActive(false);
                Player_anim.Play("idle");
                Destroy(Del);
                Instantiate(Creat, Creatpoint.position, Creatpoint.rotation);
                transform.position = new Vector3(target.transform.position.x,
        target.transform.position.y, target.transform.position.z);
                TLplaying = false;
                Time.timeScale = 1f;
                ttng = false;
                Alive = true;
            }
            
        }
        if (Alive)
        {
            if (!TLplaying)
            {
                Flip();
            }

            isAtk = false;

            ttng = false;
            Wrong.SetActive(false);
            DieAin.SetActive(false);
            Claming_timer -= Time.deltaTime;
            Go_timer += Time.deltaTime;
            Sleep_timer += Time.deltaTime;
            ATK_timer -= Time.deltaTime;
            Aplus -= Time.deltaTime;
            Y_timer -= Time.deltaTime;
            Jumping_timer -= Time.deltaTime;


            if (isDashing)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isClaming && !isJump && !TLplaying)
            {
                StartCoroutine(Dash());
            }

            //A
            if (Aplus < 0.4f)
            {
                ForD = false;
            }

            //時間歸零
            if (ATK_timer < 1.5f)
            {
                atk2 = false;
                Nwalk = false;

                if (ATK_timer < 1.2f)
                {
                    ComboCount = 0f;
                    tt = false;
                }
            }
            if (TLplaying)
            {
                run.TL = true;
            }

            Player_Rigid.isKinematic = false;

            if (!isClaming)
            {
                if (!TLplaying)
                {
                    
                    Attack();
                    move();
                    JumpJump();

                }
                music();

            }

            //爬牆
            if (wallJumpCounter <= 0)
            {

                if (canClam && !isGround && !isST)
                {
                    if (gameObject.transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0)
                    {
                        isClaming = true;
                        Claming_timer = 0.3f;
                        walk_timer = -1;
                        An = 1;
                    }

                    if (gameObject.transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0)
                    {
                        isClaming = true;
                        Claming_timer = 0.3f;
                        walk_timer = -1;
                        An = -1;
                    }
                    if (Claming_timer < 0)
                    {
                        isClaming = false;

                    }
                }

                if (isClaming == true && !isST)
                {
                    Nwalk = true;

                    Player_Rigid.useGravity = false;
                    Player_Rigid.velocity = Vector2.zero;
                    Player_anim.Play("clam_idle");
                    Player_anim.SetBool("idle", false);


                    if (An == 1 && !isST)
                    {
                        if (Input.GetKeyDown(KeyCode.A) && Input.GetAxisRaw("Horizontal") < 0)
                        {
                            Jumping_timer = 0.8f;
                            canClam = false;
                            Nwalk = true;
                            Player_anim.Play("GrabFlap");
                            Player_Rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * 6, 11);
                            myAudioSource.PlayOneShot(JumpAudio);
                            wallJumpCounter = WallJumpTime;
                            Player_Rigid.useGravity = true;
                            Clam_timer = 0;
                            walk_timer = -1;
                            An = 0;
                        }
                    }

                    if (An == -1 && !isST)
                    {
                        if (Input.GetKeyDown(KeyCode.D) && Input.GetAxisRaw("Horizontal") > 0)
                        {
                            Jumping_timer = 0.8f;
                            canClam = false;
                            Nwalk = true;
                            Player_anim.Play("GrabFlap");
                            Player_Rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * 6, 11);
                            myAudioSource.PlayOneShot(JumpAudio);
                            wallJumpCounter = WallJumpTime;
                            Player_Rigid.useGravity = true;
                            Clam_timer = 0;
                            walk_timer = -1;
                            An = 0;
                        }
                    }
                    if (Clam_timer > 0.5f)
                    {
                        Nwalk = false;
                        isClaming = false;
                        Clam_timer = 0;
                    }
                }
                else
                {

                    if (!ForD)
                    {
                        Player_Rigid.useGravity = true;
                    }

                }

            }
            else
            {
                wallJumpCounter -= Time.deltaTime;
                isClaming = false;
            }
        }
        
    }

    void FixedUpdate()  //目前沒用到
    {
        if (!isClaming)
        {
            FallingFunction();
            if (isDashing)
            {
                return;
            }
        }


    }
    private IEnumerator Dash()
    {
        Nwalk = true;
        try2.enabled = false;
        Time.timeScale = 1.5f;
        canDash = false;
        isDashing = true;
        Player_Rigid.useGravity = false;
        Player_anim.Play("Dash");
        Player_Rigid.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        try2.enabled = true;
        Time.timeScale = 1f;
        Nwalk = false;
        Player_Rigid.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    void Attack()  //攻擊
    {


        if (Input.GetMouseButtonDown(0) && isClaming == false && TimeLine && ComboCount ==0f && !isDashing)
        {
            walk_timer = -1;
            Player_anim.Play("attack_2");
            ATK_timer = 1.8f;
            atk2 = true;
            tt = true;
            ComboCount++;
            Nwalk = true;
            Jumping_timer = 0.8f;
            myAudioSource.PlayOneShot(ATKAudio);
            Vector3 m_pos = Input.mousePosition;
            m_pos.z = 25;
            Vector3 look = Camera.main.ScreenToWorldPoint(m_pos);

            if (m_pos.x > 1020 && m_pos.y > 400 && isClaming == false)
            {

                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                Player_Rigid.velocity = new Vector3(5, 8);
            }
            else if (m_pos.x > 1020 && m_pos.y < 400 && isClaming == false)
            {

                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                Player_Rigid.velocity = new Vector3(5, -8);
            }

            if (m_pos.x < 900 && m_pos.y > 400 && isClaming == false)
            {

                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                Player_Rigid.velocity = new Vector3(-5, 8);
            }
            else if (m_pos.x < 900 && m_pos.y < 400 && isClaming == false)
            {

                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                Player_Rigid.velocity = new Vector3(-5, -8);
            }

            if (m_pos.x > 900 && m_pos.x < 1020 && m_pos.y > 450 && isClaming == false)
            {

                Player_Rigid.velocity = new Vector3(0, 8);
            }
            else if (m_pos.x > 900 && m_pos.x < 1020 && m_pos.y < 400 && isClaming == false)
            {
                Player_Rigid.velocity = new Vector3(0, -8);
            }

        }

    }

    void move()  //移動
    {
        if (!Nwalk && !isJump && !isDashing)
        {
            Player_anim.SetBool("idle", true);

            if (Input.GetKey(KeyCode.D))
            {
                Player_Rigid.velocity = new Vector3(speed, Player_Rigid.velocity.y);
                if (!atk2)
                {
                    Player_anim.Play("Running");
                    Player_anim.SetBool("idle", false);
                }
                moveFace = 0;
                walk_timer += Time.deltaTime;
                walking = true;
                Plus = false;

            }
            else if (walk_timer > 3f)
            {
                Plus = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Player_Rigid.velocity = new Vector3(-speed, Player_Rigid.velocity.y);
                if (!atk2)
                {
                    Player_anim.Play("Running");
                    Player_anim.SetBool("idle", false);
                }
                moveFace = 1;
                walk_timer += Time.deltaTime;
                walking = true;
                Plus = false;

            }
            else if(walk_timer > 3f)
            {
                Plus = true;
            }

            if (walking && Plus)
            {
                Player_anim.Play("STOP");
                Player_anim.SetBool("idle", false);
                walking = false;
                Plus = false;
                walk_timer = 0f;
            }

        }
    }


    void JumpJump() //跳躍
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            myAudioSource.PlayOneShot(JumpAudio);
            Player_anim.Play("jump");
            isJump = true;
            Nwalk = true;
            walk_timer = -1;
            Jumping_timer = 0.8f;
            Player_Rigid.velocity = new Vector3(Player_Rigid.velocity.x, Jump);
        }
        else if (isJump && Jumping_timer<0)
        {
            isJump = false;
            Nwalk = false;
        }

    }

    void FallingFunction()
    {
        if (!isST)
        {
            A = this.transform.position.y;

            if (Y_timer < 0)
            {
                B = A;
                Y_timer = 0.1f;
            }
            if (A + 0.2f < B && !isGround)
            {
                Player_anim.Play("fly");
                Player_anim.SetBool("idle", true);
                Nwalk = true;
            }
        }
        
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "Ground")
        {
            isJump = false;
            Player_anim.SetBool("idle", isGround);

            isGround = true;
            Nwalk = false;
            isST = false;
            ForD = false;
        }
        if (collision.tag == "EnemyATK")
        {
            Player_anim.Play("Die");
            myAudioSource.PlayOneShot(dieAudio);
            if (x > 0)
            {
                Player_Rigid.velocity = new Vector3(-6,2);
                Player_Rigid.useGravity = false;
                try1.enabled = false;
                try2.enabled = false;
                TLplaying = true;
                Time.timeScale = 0.8f;
                Diet = 0f;
                Alive = false;
            }
            else if (x < 0)
            {
                Player_Rigid.velocity = new Vector3(6,2);
                Player_Rigid.useGravity = false;
                try1.enabled = false;
                try2.enabled = false;
                TLplaying = true;
                Time.timeScale = 0.8f;
                Diet = 0f;
                Alive = false;
            }
            
        }

        if (collision.tag == "ST_CheckPointIn")
        {
            isST = true;
            isJump = false;

        }

        if (collision.tag == "touch")
        {
            Player_Rigid.isKinematic = true;
        }

        if (collision.tag == "Musicdoor")
        {
            myAudioSource.Stop();
        }

        if (collision.tag == "RBTATK")
        {
            isAtk = true;
        }

        if (collision.tag == "END")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.tag == "TimeLine")
        {
            TimeLine = true;
        }

        if (collision.tag == "Door")
        {
            Player_anim.Play("idle");

        }
        if (collision.tag == "CanDown")
        {

        }
    }

    void OnTriggerStay(Collider collision)  //穿地
    {
        if (collision.tag == "Stairs")
        {
            isST = true;

            if (isST)
            {

                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) && !isGround)
                {

                    if (Input.GetKey(KeyCode.A))
                    {
                        Player_anim.Play("Running");
                        Sting = true;
                        Nwalk = true;
                        ForD = true;
                        Aplus = 0.5f;

                        Player_Rigid.velocity = new Vector3(-0, -4);
                        Player_Rigid.useGravity = false;
                        Player_anim.SetBool("idle", false);

                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        Player_anim.Play("Running");
                        Sting = true;
                        Nwalk = true;
                        ForD = false;

                        Player_Rigid.velocity = new Vector3(2, 5);

                        Player_anim.SetBool("idle", false);
                    }

                }

            }

        }
    }

    void music()
    {
        if (Input.GetKeyDown("escape") && Time.timeScale == 1f)
        {
            myAudioSource.Pause(); 
        }
        else if (Time.timeScale == 0f)
        {
            myAudioSource.Play();
        }
        
    }

    private bool CheckJumpWalkLeft()
    {
        LayerMask mask = LayerMask.GetMask("JumpWall");


        if (Physics.Raycast(
            new Ray(transform.position, Vector3.left),
            out RaycastHit hitInfo, groundDetectLength, mask))
        {

            return true;

        }
        else
        {
            return false;
        }
        


    }

    private bool CheckJumpWalkRight()
    {
        LayerMask mask = LayerMask.GetMask("JumpWall");

        if (Physics.Raycast(
            new Ray(transform.position, Vector3.right),
            out RaycastHit hitInfo, groundDetectLength, mask))
        {
            return true;
        }
        else
        {
            return false;
            
        }

    }

    private void OnDrawGizmos() //可看見偵測的線  //沒用ㄌ
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * groundDetectLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * groundDetectLength);

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "BOX")
        {
        }
        if (collision.tag == "Door")
        {
            /*C.SetActive(true);*/
        }

        if (collision.tag == "Ground")
        {
            Player_anim.SetBool("idle", isGround);
            isGround = false;
            walk_timer = -1;
        }

    }

    private void Flip()  //圖片翻轉
    {

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (inputHorizontal > 0 && Nwalk == false)
        {
            x = 1f;
            gameObject.transform.localScale = new Vector3(x, 1f, 1f);
            CheckJumpWalkRight();
            canClam = CheckJumpWalkRight();

        }

        if (inputHorizontal < 0 && Nwalk == false)
        {
            x = -1f;
            gameObject.transform.localScale = new Vector3(x, 1f, 1f);
            CheckJumpWalkLeft();
            canClam = CheckJumpWalkLeft();
        }



    }






}
