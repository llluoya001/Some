using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_T : MonoBehaviour
{
    public GameObject Open,End;
    float Z;
    // Start is called before the first frame update
    void Start()
    {
        PlayerContorl.TLplaying = true;
        Open.SetActive(true);
        End.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Z += Time.deltaTime;

        if (Z > 7)
        {
            End.SetActive(true);

            if (Z > 8)
            {
                Open.SetActive(false);

                if (Z > 9)
                {
                    End.SetActive(false);
                    PlayerContorl.TLplaying = false;
                }
            }
            
        }
    }
}
