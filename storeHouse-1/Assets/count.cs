using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    public static float x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x += Time.deltaTime;
    }
}
