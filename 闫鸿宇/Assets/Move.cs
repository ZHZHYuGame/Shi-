using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int vv;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            this.transform.position += this.transform.right * 10 * Input.GetAxis("Horizontal") * Time.deltaTime;
            Camera.main.transform.position += this.transform.right * 10 * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position += this.transform.up * 3;
        }

        if (this.transform.position.y >= -4)
        {
            if (this.transform.position.x >= 100 || this.transform.position.x <= -100)
            {
                Time.timeScale = 0;
                GameObject.Find("win").GetComponent<Text>().text = "You Win";
            }
        }
        else
        {
            Time.timeScale = 0;
            GameObject.Find("win").GetComponent<Text>().text = "Game Over";
        }
        GameObject.Find("score").GetComponent<Text>().text = vv.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            vv += 20;
        }
    }
}
