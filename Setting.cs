using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public TMP_Text leveltext;
    public TMP_Text[] listtext;
    public GameObject leftobj;
    int level = 0, target = 0;
    public GameObject on;
    public GameObject off;
    bool active;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        on.SetActive(true);
        off.SetActive(false);
        PlayerPrefs.SetInt("max", 0);
        PlayerPrefs.SetInt("count", listtext.Length);
        if (level == 0) leftobj.SetActive(false);
        check();
    }
    private void FixedUpdate()
    {
        reshow();
    }
    public void onoff()
    {
        on.SetActive(active);
        off.SetActive(!active);
        active = !active;
    }
    public void add(int abs)
    {
        int num = target == 6 ? 5 : 1;
        num = PlayerPrefs.GetInt(target + "," + level) + num * abs;
        if ((target == 2 || target == 5) && num < 1) num = 1;
        else if ((target == 2 || target == 5) && num > 4) num = 4;
        else if (num < 0) num = 0;
        PlayerPrefs.SetInt((target + "," + level), num);
    }
    public void StartGame()
    {
        if (check()) SceneManager.LoadScene(1);
    }
    public bool check()
    {
        int maxlevel = 0;
        while (true)
        {
            for (int i = 0; i < listtext.Length; i++)
            {
                if (PlayerPrefs.GetInt(i + "," + maxlevel) == 0)
                {
                    PlayerPrefs.SetInt("max", maxlevel);
                    if (maxlevel > 0) return true;
                    else return false;
                }
            }
            maxlevel++;
        }
    }

    void reshow()
    {
        levelword();
        for (int i = 0; i < listtext.Length; i++)
            listtext[i].text = PlayerPrefs.GetInt(i + "," + level).ToString();
    }
    void levelword()
    {
        check();
        if (active)
        {
            leveltext.text = "level " + (level + 1);
            Color color = new Color();
            color.r = color.g = color.b = 225;
            color.a = level<PlayerPrefs.GetInt("max")?1:0.5f;
            leveltext.color = color;
        }
        else
        {
            leveltext.text = "Max level " + (PlayerPrefs.GetInt("max"));
        }
    }

    public void right()
    {
        level++;
        check();
        if (level != 0) leftobj.SetActive(true);
    }
    public void clear()
    {
        level = 0;
        PlayerPrefs.DeleteAll();
    }
    public void left()
    {
        level--;
        if (level == 0) leftobj.SetActive(false);
    }
    public void click(int i)
    {
        target = i;
    }
}