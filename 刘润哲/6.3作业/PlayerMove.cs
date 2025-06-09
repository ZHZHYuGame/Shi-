using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;
[LuaCallCSharp]
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Move(Vector3 dir)
    {
        transform.Translate(dir * Time.deltaTime * moveSpeed);

    }
    //public delegate void OnCollisionEnterDelegate(Collision collision);
    //public event OnCollisionEnterDelegate OnCollisionEnterEvent;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // ´¥·¢Åö×²ÊÂ¼þ
    //    OnCollisionEnterEvent?.Invoke(collision);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            Debug.Log(111);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
