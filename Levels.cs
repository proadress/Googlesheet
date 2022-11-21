using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public TMP_Text leveltext;
    public int deathtime, waittime;
    public ObjectPool[] pool;
    Puase puase;
    int level = 0;
    float start_point = 5f, start_range = 1.5f;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        reload();
        StartCoroutine(wait(waittime/10));
        StartCoroutine(Spawner(0));
        StartCoroutine(Spawner(1));
    }
    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("state") == 0)
        {
            if (PlayerPrefs.GetInt("life") == 0)
                StartCoroutine(death_wait());
            else if (PlayerPrefs.GetInt("score") == 5)
            {
                level++;
                if (PlayerPrefs.GetInt("max") == level) SceneManager.LoadScene(2);
                StartCoroutine(wait(waittime/10));
            }
        }
    }
    IEnumerator Spawner(int n)
    {
        int a = n * 3;
        yield return new WaitForSecondsRealtime(PlayerPrefs.GetInt((a + 1) + "," + level)/10);
        if (PlayerPrefs.GetInt("state") == 0)
        {
            float range = Random.Range(start_range, -start_range);
            float dir = PlayerPrefs.GetInt((a + 2) + "," + level);
            Vector3 vecangle =
            dir == 1 ? new Vector3(start_point, range, 0) :
            dir == 2 ? new Vector3(range, start_point, 0) :
            dir == 3 ? new Vector3(-start_point, range, 0) :
            dir == 4 ? new Vector3(range, -start_point, 0) : Vector3.zero;
            pool[n].ReUse(vecangle);
        }
        StartCoroutine(Spawner(n));
    }
    void reload()
    {
        PlayerPrefs.SetInt("state", 0);
        leveltext.text = "level " + (level + 1);
        PlayerPrefs.SetInt("level", level);
    }
    IEnumerator wait(float wait)
    {
        PlayerPrefs.SetInt("state", 2);
        yield return new WaitForSecondsRealtime(wait);
        PlayerPrefs.SetInt("score", 0);
        if (PlayerPrefs.GetInt("state") != 1) reload();
    }
    IEnumerator death_wait()
    {
        PlayerPrefs.SetInt("state", 2);
        yield return new WaitForSecondsRealtime(deathtime/10);
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(2);
    }
}
