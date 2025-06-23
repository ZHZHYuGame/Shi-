using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI����
/// 1.ui�Ļ�����ʾ����
/// 2.UI���¼�����
/// 3.UI�ĺ�����
/// 4.Ui�ļ��ش���
/// </summary>
public class UIBase : MonoBehaviour
{
 
    public virtual void Show() { }
   
    public virtual void Close() { }
    /// UIģ��ĺ��Ĵ����߼�
    /// 1.�ⲿ֪ͨ���״̬
    /// 2.�ڲ���ȡ��㻺�棨��֤���״̬����׼ȷ
    /// </summary>
    public virtual void RegRedPoint() { }
    public virtual void UnRegRedPoint() { }
    public virtual void AddEventListener() { }
    public virtual void UnAddEventListener() { }
    public virtual void LoadFinish() { }
}
