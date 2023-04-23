using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform target;
    public GameObject tager;

    Transform _Transform;

    Rigidbody Player_Rigid;

    // Start is called before the first frame update
    void Start()
    {
        Player_Rigid = GetComponent<Rigidbody>();
        _Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTriggerStay(Collider other)
    {
        //鉤繩系統

        if (other.tag == "Touch" )
        {
            Vector3 followPos = new Vector3(target.position.x + 4.5f, target.position.y, _Transform.position.z);
            _Transform.position = followPos;
        }

    }*/
}
