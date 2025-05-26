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
        // 更新显示的数值
        if (valueText != null)
            valueText.text = value.ToString();

        // 绘制到下一个节点的连线
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
