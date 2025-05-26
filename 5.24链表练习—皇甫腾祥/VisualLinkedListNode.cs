using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualLinkedListNode : MonoBehaviour
{
    public int value;
    public VisualLinkedListNode nextNode;

    [SerializeField] private TextMesh valueText;
    [SerializeField] private LineRenderer connectionLine;

    void Update()
    {
        // ������ʾ����ֵ
        if (valueText != null)
            valueText.text = value.ToString();

        // ���Ƶ���һ���ڵ������
        if (nextNode != null && connectionLine != null)
        {
            connectionLine.positionCount = 2;
            connectionLine.SetPosition(0, transform.position);
            connectionLine.SetPosition(1, nextNode.transform.position);
        }
        else if (connectionLine != null)
        {
            connectionLine.positionCount = 0;
        }
    }
}
