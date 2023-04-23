using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ele : MonoBehaviour
{
    public Transform target;

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

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "touch" )
        {
            Debug.Log("秒");
            Vector3 followPos = new Vector3(_Transform.position.x, target.position.y , _Transform.position.z);
            _Transform.position = followPos;

        }

        

    }
}
