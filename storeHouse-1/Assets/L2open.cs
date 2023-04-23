using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2open : MonoBehaviour
{
    public GameObject RB;

    // Start is called before the first frame update
    void Start()
    {
        RB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            RB.SetActive(true);
        }
    }
}
