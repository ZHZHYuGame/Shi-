using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ƶ�
/// </summary>
public class HeroMove : MonoBehaviour
{
    //����ƶ��ٶ�
    public float speed = 2f;
    /// <summary>
    /// ����ƶ�����
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void Move(float h, float v)
    {

        Vector3 pos = new Vector3(h, 0, v);

        transform.Translate(pos);


    }
}