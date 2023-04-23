using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class OpenTLBoss : MonoBehaviour
{
    public BoxCollider try1;
    float y;
    public PlayableDirector playableDirector;
    public GameObject AD;

    void Update()
    {
        y += Time.deltaTime;

        if (y < 9)
        {
            PlayerContorl.TLplaying = true;
            run.TL = true;
            AD.SetActive(false);
        }
        if (y >= 9)
        {
            PlayerContorl.TLplaying = false;
            run.TL = false;
            AD.SetActive(true);
            Destroy(this.gameObject);
        }   

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playableDirector.Play();
            try1.enabled = false;
            y = 0;
        }
    }
}
