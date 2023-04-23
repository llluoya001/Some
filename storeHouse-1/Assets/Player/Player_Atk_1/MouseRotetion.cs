using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotetion : MonoBehaviour
{
    public GameObject eff;
    public GameObject light;
    public float F;


    float ATK_timer;
    float eff_timer;

    // Start is called before the first frame update
    void Start()
    {
        eff.SetActive(false);
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerContorl.Alive)
        {
            eff.SetActive(false);
            light.SetActive(false);
        }
        if (PlayerContorl.Alive)
        {
            ATK_timer += Time.deltaTime;
            eff_timer += Time.deltaTime;


            if (boxC.N)
            {
                light.SetActive(true);
            }

            if (ATK_timer > 0.2f)
            {
                eff.SetActive(false);
                light.SetActive(false);
            }

            if (!PlayerContorl.tt && PlayerContorl.TimeLine && !PlayerContorl.TLplaying)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ATK_timer = 0;
                    eff.SetActive(true);

                    Vector3 m_pos = Input.mousePosition;
                    m_pos.z = F;
                    Vector3 look = Camera.main.ScreenToWorldPoint(m_pos);
                    transform.rotation = Quaternion.LookRotation(look - transform.position, Vector3.back);


                }
            }
        }

    }


}
