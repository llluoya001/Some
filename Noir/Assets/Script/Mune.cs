using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class Mune : MonoBehaviour
{


    public void Click()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void backto()
    {
        SceneManager.LoadScene(0);
    }
}
