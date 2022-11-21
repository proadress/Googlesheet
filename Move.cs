using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float speed;
    int way = 1;
    private void FixedUpdate()
    {
        speed = PlayerPrefs.GetInt(6 + "," + PlayerPrefs.GetInt("level"));
        if(PlayerPrefs.GetInt("state") != 1)transform.Rotate(0, 0, speed * Time.deltaTime * way);
    }
    public void ChangeDirection()
    {
        way = -way;
    }
}
