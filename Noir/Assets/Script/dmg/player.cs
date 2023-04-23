using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    float player_hp;
    public Image HP;
    private Animator Player_anim;

    // Start is called before the first frame update
    void Start()
    {
        player_hp = 1;
        Player_anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Drink();
        if (Input.GetKeyDown(KeyCode.O))
        {
            player_hp = 100000;
        }
        if (player_hp < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerEnter(Collider collision)  //是否在地面
    {
        if (collision.tag == "RBTATK")
        {
            player_hp -= 0.04f;
            HP.GetComponent<Image>().fillAmount -= 0.04f;
            Player_anim.SetTrigger("Hurt1");

        }

    }
    void Drink() //喝血瓶
    {
        if (Input.GetKeyDown(KeyCode.X) && BoxCat.HPbottle > 0)
        {
            BoxCat.HPbottle--;
            player_hp += 0.2f;
            HP.GetComponent<Image>().fillAmount += 0.2f;
        }
    }
}
