using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    int Hp_bottle;
    public Text HP;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = BoxCat.HPbottle.ToString();
    }
}
