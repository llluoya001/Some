using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intoEle : MonoBehaviour
{
    private Animator anim;
    float _Time;
    bool creatTime;
    float number = 0;
    public Transform CreatPoint;
    public Transform CreatPoint_2;
    public GameObject Emeny;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        creatTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        _Time += Time.deltaTime;
        
    }

    void OnTriggerEnter(Collider collision)  //是否進入電梯
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("in", true);
        }
        if (collision.tag == "creat")
        {
            
            _Time = 0;
            if (_Time < 3 && number < 5 && creatTime == true)
            {
                Instantiate(Emeny, CreatPoint.position, CreatPoint.rotation);
                Instantiate(Emeny, CreatPoint_2.position, CreatPoint_2.rotation);
            }
            else creatTime = false;
        }

    }

    void OnTriggerStay(Collider collision)  //是否進入關卡
    {
        

    }
}
