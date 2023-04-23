using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QtoToms : MonoBehaviour
{
    private Animator _anim;
    public GameObject target;
    float x;
    bool Y,In;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        target.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        x += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && In)
        {
            x = 0;
            _anim.Play("DoorOpenR");
            target.SetActive(true);
            PlayerContorl.TLplaying = true;

            Y = true;
        }

        if (x > 1 && Y && In)
        {
            PlayerContorl.TLplaying = false;
            SceneManager.LoadScene(3);
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
