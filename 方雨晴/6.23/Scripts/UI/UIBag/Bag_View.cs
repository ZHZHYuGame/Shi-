using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag_View : UIBase
{
    public override void Close()
    {
        base.Close();
    }
    public override void Show()
    {
        base.Show();
    }
    public override void LoadFinish()
    {
        base.LoadFinish();
    }

    /// UIģ��ĺ��Ĵ����߼�
    /// 1.�ⲿ֪ͨ���״̬
    /// 2.�ڲ���ȡ��㻺�棨��֤���״̬����׼ȷ
    /// </summary>
    public override void RegRedPoint()
    {
        RedPointMgr<int>.ins.RegPointToUIHandle(RedPoinType.GoodUse, GoodUesHandle);
        RedPointMgr<string>.ins.RegPointToUIHandle(RedPoinType.GoodUse, GoodUesHandle);
    }

    private void GoodUesHandle(string obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// ��Ʒʹ�ú����ʾ
    /// </summary>
    private void GoodUesHandle(int obj)
    {
        //��Ʒʹ�õ�λ��
        int usedID = obj;

    }


   
    public override void UnRegRedPoint()
    {

    }
    public override void AddEventListener()
    {
    }

    public override void UnAddEventListener()
    {

    }

   
}
