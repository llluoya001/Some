using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_Broke : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip ATKAudio;
    private Animator _anim;
    Collider Door_Collider;
    bool Door_break;
    int n=0;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        Door_Collider = GetComponent<BoxCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Door_break == true &&n==0)
        {
            myAudioSource.PlayOneShot(ATKAudio);
            n=1;
        }
    }

    void OnTriggerEnter(Collider collision)  //ê•î€ç›ínñ 
    {

        if (collision.tag == "ATK")
        {
            Door_break = true;
            _anim.Play("door_down");
            Door_Collider.enabled = false;

        }

    }
}
