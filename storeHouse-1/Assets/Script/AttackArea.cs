using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, x);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

    }
}
