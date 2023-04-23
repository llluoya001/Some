using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorB : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider collision)  //是否在箱子旁
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.C))
        {
            _anim.SetTrigger("open");
        }

    }
}
