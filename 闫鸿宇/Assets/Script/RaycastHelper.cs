using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLuaTest;

public class RaycastHelper : MonoBehaviour
{
    public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, out RaycastHit hitInfo)
    {
        return Physics.Raycast(origin, direction, out hitInfo, maxDistance);
    }

}
