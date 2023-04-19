using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ͨ������������ĵ������߼�����ҵĴ���
public class MyObserver : MonoBehaviour
{
    //�������е���ұ任������������ҽ����
    public Transform player;
    //����һ��bool��������ʾ����Ƿ�����ɨ��
    //���Ƿ�������������ߵĴ�����������
    bool m_IsPlayerInRange;
    // ������Ϸ�����ű���������Ϊ�˵���
    // ��Ϸ�����ű��еĹ��з�����������
    //public GameEnding gameEnding;
    public MyGameEnding myGameEnding;

    //���봥�����¼�
    //��ҽ������ߴ���������
    //���Ŀ���ֵ
    private void OnTriggerEnter(Collider other)
    {
        //������봥��������������
        if(other.transform == player)
        {
            //���ر���ֵ����Ϊ��
            m_IsPlayerInRange = true;  
        }
    }

    //�뿪�������¼�
    private void OnTriggerExit(Collider other)
    {
        //�������������е������
        if (other.transform == player) {
            //�뿪ʱ�����ÿ���ֵΪ��
            m_IsPlayerInRange = false;
        }
    }

    //�� Update �м�ؿ��ص�ֵ��һ����ҽ������ߴ���������
    //ִ�ж�Ӧ���߼�
    private void Update()
    {
        //�������ѱ�����
        if (m_IsPlayerInRange)
        {
            //����Ͷ�������õ��ķ���ʸ��
            Vector3 direction = player.position - transform.position + Vector3.up;
            //��������
            Ray ray = new Ray(transform.position, direction);

            //���߻��ж��󣬰���������ײ��Ϣ
            RaycastHit raycastHit;

            //ʹ������ϵͳ�������ߣ������������
            // �����һ�� if
            // out ����ڶ���������������������Դ������ݵ�������
            if (Physics.Raycast(ray, out raycastHit))
            {
                //��������������
                if (raycastHit.collider.transform == player)
                {
                    //���� GameEnding �ű��е�ץ����ҵķ���
                    myGameEnding.CaughtPlayer();
                }
            }
        }
        
    }

}
