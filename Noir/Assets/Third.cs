using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third : MonoBehaviour
{
    public GameObject one,two,three,Down;
    public static bool _O, _T, _TH;
    float x,y;

    // Start is called before the first frame update
    void Start()
    {
        _O = false;
        _T = false;
        _TH = false;
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        y -= Time.deltaTime;
        if (!PlayerContorl.Alive)
        {
            _O = false;
            _T = false;
            _TH = false;
        }

        if (PlayerContorl.Alive)
        {
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _O = true;
        }
    }
}
