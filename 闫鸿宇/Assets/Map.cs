using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Transform tra;
    private void Awake()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Square"));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool a = false;
    int b = 0;
    bool c = false;
    int d = 0;
    // Update is called once per frame
    void Update()
    {
        tra = GameObject.Find("Player").transform;
        if (tra.position.x % 12 >= 1 && b == 0)
        {
            a = true;
            
        }
        else if(Mathf.Abs(tra.position.x) % 12 <= 1 && b != 0)
        {
            b = 0;
        }
        if (a)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Square"));
            go.transform.position = new Vector3(((Mathf.Abs(tra.position.x) / 12) + 1) * 12, go.transform.position.y + Random.Range(0, 2), 0);
            a = false;
            b++;
        }
        if (Mathf.Abs( tra.position.x) % 12 >= 1 && d == 0)
        {
            c = true;

        }
        else if (Mathf.Abs(tra.position.x) % 12 <= 1 && d != 0)
        {
            d = 0;
        }
        if (c)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Square"));
            go.transform.position = new Vector3(((Mathf.Abs(tra.position.x) / 12) + 1) * -12, go.transform.position.y + Random.Range(0, 2), 0);
            c = false;
            d++;
        }
    }
}
