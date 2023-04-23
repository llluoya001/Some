using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 3f;
    public float gravityScale = 1f;
    private float globalGravity = -9.81f;


    Rigidbody player_rb;

    void Awake()
    {
        player_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            if (player_rb.velocity.y < 0)
            {
                //player_rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                player_rb.AddForce(gravity * fallMultiplier, ForceMode.Acceleration);
            }
            else
                player_rb.AddForce(gravity, ForceMode.Acceleration);
        }
           
    }
}
