using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_RBT : MonoBehaviour
{
    float health_RBT;
    private Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        health_RBT = 5;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health_RBT < 0)
        {
            ani.SetTrigger("Die");
            Destroy(gameObject, 1);
        }
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "ATK")
        {
            health_RBT--;
        }

    }
}
