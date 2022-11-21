using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject[] pool;
    int life;
    private void Awake()
    {
        PlayerPrefs.SetInt("life", life = pool.Length);
        for (int i = 0; i < pool.Length; i++)
            pool[i].SetActive(true);
    }
    private void FixedUpdate()
    {
        if (life != PlayerPrefs.GetInt("life"))
        {
            life = PlayerPrefs.GetInt("life");
            for (int i = 0; i < pool.Length; i++)
            {
                if(i<life)pool[i].SetActive(true);
                else pool[i].SetActive(false);
            }
        }

    }
}
