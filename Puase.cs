using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puase : MonoBehaviour
{
    public GameObject ob;
    int state = 0;
    private void Awake()
    {
        ob.SetActive(false);
    }
    public void pause()
    {
        if (PlayerPrefs.GetInt("state") != 1)
        {
            state = PlayerPrefs.GetInt("state");
            PlayerPrefs.SetInt("state", 1);
            ob.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("state", state);
            ob.SetActive(false);
        }
    }public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
