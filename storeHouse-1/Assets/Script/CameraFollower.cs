using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    Transform _Transform;
    

    public float xOffset;
    public float yOffset;



    // Start is called before the first frame update
    void Start()
    {
        _Transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        float y = target.position.y + yOffset;
        float x = target.position.x + xOffset;


        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 followPos = new Vector3(target.position.x, _Transform.position.y, _Transform.position.z);
            _Transform.position = followPos;
        }
        else
        {
            Vector3 followPos = new Vector3(target.position.x, target.position.y + 0.5f, _Transform.position.z);
            _Transform.position = followPos;
        }



    }

   
}
