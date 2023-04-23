using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playereffect : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip JumpAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
