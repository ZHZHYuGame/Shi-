using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWay : MonoBehaviour
{
    public float speed = 10f;
    public void Move(float h,float v)
    {
        var v3=new Vector3(h,0,v)*speed*Time.deltaTime;
        transform.Translate(v3);
    }
}
