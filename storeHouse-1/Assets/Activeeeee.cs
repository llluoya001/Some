using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activeeeee : MonoBehaviour
{
    public GameObject Key;

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Key.SetActive(true);
        }

    }
}
