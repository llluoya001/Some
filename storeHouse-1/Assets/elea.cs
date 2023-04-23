using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elea : MonoBehaviour
{
    private AudioSource myAudioSource;
    public AudioClip JumpAudio;
    public GameObject Ani;
    public GameObject Black;
    float y;
    bool Stoping,canIn;

    // Start is called before the first frame update
    void Start()
    {
        Stoping = false;
        Ani.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        myAudioSource = GetComponent<AudioSource>();
        y += Time.deltaTime;

        if (y > 2 && Stoping)
        {
            Black.SetActive(true);
        }

        if (y > 4 && Stoping)
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Q) && canIn)
        {
            y = 0f;
            myAudioSource.PlayOneShot(JumpAudio);
            Stoping = true;
            Ani.SetActive(true);

        }

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {

            canIn = true;
            

        }
    }
}
