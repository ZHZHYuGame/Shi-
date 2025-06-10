using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string bin = "11001010";
        string hex = "";
        hex = string.Format("{0:X}", Convert.ToInt32(bin, 2)); // CA
        hex = "OX" +string.Format("{0:X}", Convert.ToInt32(bin, 2)); // 0XCA
        hex = "OX" + string.Format("{0:X4}", Convert.ToInt32(bin, 2)); // OX00CA
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
