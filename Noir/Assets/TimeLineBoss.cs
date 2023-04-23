using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimeLineBoss : MonoBehaviour
{
    public BoxCollider try1;
    float y;
    public PlayableDirector playableDirector;
    bool In = false;

    void Update()
    {
        if (In)
        {
            y += Time.deltaTime;
            if (y < 5)
            {
                PlayerContorl.TLplaying = true;
                run.TL = true;
            }
        }
        
        if (y > 5)
        {
            PlayerContorl.TLplaying = false;
            run.TL = false;
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playableDirector.Play();
            PlayerContorl.TLplaying= true;
            run.TL = true;
            try1.enabled = false;
            In = true;
            y = 0f;

        }
    }

}
