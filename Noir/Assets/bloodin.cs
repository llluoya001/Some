using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodin : MonoBehaviour
{
    public GameObject blood;
    void Update()
    {
       
        if (PlayerContorl.ttng)
        {
            blood.SetActive(false);
        }

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "EnemyDie")
        {
            blood.SetActive(true);

        }
    }

}
