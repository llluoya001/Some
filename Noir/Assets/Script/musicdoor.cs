using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicdoor : MonoBehaviour
{
    private AudioSource myAudioSource;
    int playTime;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)  //是否進入關卡
    {
        if (other.tag == "Player" && playTime < 1)
        {
            myAudioSource.Play();
            playTime = 2;
        }

    }
}
