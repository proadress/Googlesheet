using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool wake;
    float xspeed, yspeed;
    private void Start()
    {
        wake = true;
        xspeed = yspeed = (gameObject.CompareTag("Cube")) ?
        PlayerPrefs.GetInt(3 + "," + PlayerPrefs.GetInt("level")) / 10 :
        PlayerPrefs.GetInt(0 + "," + PlayerPrefs.GetInt("level")) / 10;
        int angle = transform.position.x == -5 ? 0 :
        transform.position.y == -5 ? 90 :
        transform.position.x == 5 ? 180 :
        transform.position.y == 5 ? 270 : 45;
        xspeed = angle == 0 ? xspeed : angle == 180 ? -xspeed : 0;
        yspeed = angle == 90 ? yspeed : angle == 270 ? -yspeed : 0;
        GetComponent<Rigidbody2D>().velocity = new Vector3(xspeed, yspeed, 0);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        if (!wake) Start();
        if (Mathf.Abs(transform.position.x) > 5 ||
        Mathf.Abs(transform.position.y) > 5 ||
        PlayerPrefs.GetInt("state") == 2)
        {
            GameObject.Find(transform.parent.name).GetComponent<ObjectPool>().Recovery(gameObject);
            wake = false;
        }
        else if (PlayerPrefs.GetInt("state") == 1)
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        else if (PlayerPrefs.GetInt("state") == 0)
            GetComponent<Rigidbody2D>().velocity = new Vector3(xspeed, yspeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Role"))
        {
            wake = false;
            GameObject.Find(transform.parent.name).GetComponent<ObjectPool>().Recovery(gameObject);

            if (gameObject.CompareTag("Cube")) PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
            else
            {
                if (PlayerPrefs.GetInt("life") > 0)
                {
                    PlayerPrefs.SetInt("life", PlayerPrefs.GetInt("life") - 1);
                }
            }
        }
        else if (other.gameObject.CompareTag("Cube"))
        {
            wake = false;
            GameObject.Find(transform.parent.name).GetComponent<ObjectPool>().Recovery(gameObject);
        }
    }
}
