using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class file : MonoBehaviour
{
    public TMP_InputField field;
    public string presetfile = "{\"list\":[[15,15,20],[100,50,50],[3,4,1],[10,10,10],[30,30,30],[3,3,3],[150,200,300]]}";
    private void Start()
    {
        if (PlayerPrefs.GetInt("0,0") == 0)
        {
            inputt(presetfile);
        }
    }
    public void output()
    {
        playerDataType p = new playerDataType();
        p.list = outputt();
        string s = JsonConvert.SerializeObject(p);
        GUIUtility.systemCopyBuffer = s;
    }
    List<List<int>> outputt()
    {
        List<List<int>> list = new List<List<int>>();
        for (int i = 0; i < PlayerPrefs.GetInt("count"); i++)
        {
            List<int> l = new List<int>();
            for (int j = 0; j < PlayerPrefs.GetInt("max"); j++)
            {
                l.Add(PlayerPrefs.GetInt(i + "," + j));
            }
            list.Add(l);
        }
        return list;
    }
    public void input()
    {
        inputt(field.text);
    }
    public void inputt(string s)
    {
        playerDataType p = new playerDataType();
        if (JsonConvert.DeserializeObject(s) != null)
        {
            p = JsonConvert.DeserializeObject<playerDataType>(s);

            for (int i = 0; i < PlayerPrefs.GetInt("count"); i++)
            {
                for (int j = 0; j < p.list[i].Count; j++)
                {
                    PlayerPrefs.SetInt((i + "," + j), p.list[i][j]);
                }
            }
        }
    }
}
public class playerDataType
{
    public List<List<int>> list = new List<List<int>>();
}
