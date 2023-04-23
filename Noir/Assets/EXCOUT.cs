using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EXCOUT : MonoBehaviour
{
    public GameObject Menu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Back()
    {
        Menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Menuu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
