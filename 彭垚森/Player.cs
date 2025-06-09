using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;

[LuaCallCSharp]
public class Player : MonoBehaviour, IPointerDownHandler
{
    public static void Test3(GameObject player)
    {
        Debug.Log("碰撞" + player.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("点击到"+hit.point);
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Col")
        {
            Test3(collision.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        // if (h != 0 || v != 0)
        // {
        //     transform.position += new Vector3(h, 0, v) * Time.deltaTime * 3;
        // }
    }
}
