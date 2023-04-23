using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    Transform _Transform;


    public float x,z;
    public float yOffset;



    // Start is called before the first frame update
    void Start()
    {
        _Transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        float y = target.position.y;


        if (y < 3.5f)
        {
            Vector3 followPos = new Vector3(target.position.x, z, _Transform.position.z);
            _Transform.position = followPos;
        }
        if (y > 3.5f)
        {
            Vector3 followPos = new Vector3(target.position.x, x, _Transform.position.z);
            _Transform.position = followPos;

        }



    }
}
