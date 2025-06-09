using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家移动
/// </summary>
public class HeroMove : MonoBehaviour
{
    //玩家移动速度
    public float speed = 2f;
    /// <summary>
    /// 玩家移动方法
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void Move(float h, float v)
    {

        Vector3 pos = new Vector3(h, 0, v);

        transform.Translate(pos);


    }
}