using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject[] pool;
    int score = 0;
    private void Awake()
    {
        resetting();
    }
    void resetting()
    {
        PlayerPrefs.SetInt("score", 0);
        for (int i = 0; i < pool.Length; i++)
            pool[i].SetActive(false);
    }
    private void FixedUpdate()
    {
        if (score != PlayerPrefs.GetInt("score"))
        {
            score = PlayerPrefs.GetInt("score");
            for (int i = 0; i < pool.Length; i++)
            {
                if (i < score) pool[i].SetActive(true);
                else pool[i].SetActive(false);
            }
        }

    }
}
