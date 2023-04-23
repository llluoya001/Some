using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ACT : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public BoxCollider try1;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playableDirector.Play();
            try1.enabled = false;

        }
    }
}
