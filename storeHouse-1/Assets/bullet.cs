using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,1);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
