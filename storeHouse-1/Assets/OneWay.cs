using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour
{
    public bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<MeshCollider>().enabled = isUp;
        }
    }
}
