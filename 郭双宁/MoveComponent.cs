using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float speed = 10f;
    
    public void Move(float h,float v)
    {
        Vector3 movement = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
