using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject target,End;
    float x;
    bool Y, In;
    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
        End.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Y)
        {
            x += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && In)
        {
            x = 0;
            target.SetActive(true);
            PlayerContorl.TLplaying = true;
            Y = true;
        }

        if (x > 0.5f)
        {
            End.SetActive(true);
        }

        if (x > 3 && Y && In)
        {
            PlayerContorl.TLplaying = false;
            SceneManager.LoadScene(0);
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {

            In = true;
        }
    }
}
