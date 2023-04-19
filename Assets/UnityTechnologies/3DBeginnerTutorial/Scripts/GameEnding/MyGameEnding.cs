using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameEnding : MonoBehaviour
{
    // ����һ�����ر������洢�û��Ƿ��ڴ�������
    bool m_IsPlayerAtExit;
    // ����һ����������������ȡ�û���ɫ
    public GameObject player;

    // ���� ͸���ȵ�ʱ��
    public float fadeDuration = 1.0f;
    // ��ʱ��
    float m_Timer;
    // ��������ȫ��͸��״̬����ʾ���� UI ��ʱ��
    public float displayImageDuration = 1.0f;

    // ����һ�� CanvasGroup , ������ȡ UI �е�ʵ���������� UI ��ͼ���͸����
    public CanvasGroup exitBackgroundImageCanvasGroup;
    //��ʾ����ͼƬ���������
    public Image image;


    //  ��һ��bool ������������Ƿ񱻵���ץ��
    bool m_IsPlayerCaught;
    // �����������󣬷ֱ��ʾ��Ӳ���л�ȡ����ͼƬ�ز�
    Sprite spriteCaught;
    Sprite spriteWon;
    private void Start()
    {
        // ʹ�� Resource.Load ����������ֱ�Ӷ�ȡ��Ŀ�ļ��� Assets/Resources �е��ļ�
        // ���ļ���Ϊ����
        spriteCaught = Resources.Load<Sprite>("Caught");
        spriteWon = Resources.Load<Sprite>("Won");
    }

    //�����ץ�������ÿ���ֵΪ��
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    //�������¼�
    private void OnTriggerEnter(Collider other)
    {
        // ����ʹ�������ײ�����봥������������Ҷ���
        if (other.gameObject == player)
        {
            //����������Ϊ true
            m_IsPlayerAtExit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ����û������������
        if (m_IsPlayerAtExit)
        {
            //���ý�����Ϸ����
            EndLevel(spriteWon, false);
        }
        //�����ұ�ץ��
        else if (m_IsPlayerCaught)
        {
            //���ý�����Ϸ�ķ���
            EndLevel(spriteCaught, true);
        }
    }

    // ������ǰ�ؿ�����ǰ��Ϸֻ��һ�� scene �����൱�ڽ�����Ϸ
    // ����1������UI��Ӧ����Ϸ����
    // ����2���Ƿ����¿�ʼ
    void EndLevel(Sprite sprite, bool doRestart)
    {
        // ��ʱ������ Update ������
        m_Timer += Time.deltaTime;
        //ָ������UI Image �������ʾ��ͼƬԴ
        image.sprite = sprite;
        // �𽥸���͸����
        // ͸���� alpha �� 0 ��1������ȫ͸������ȫ��͸��������1 �Ժ������ȫ��͸��
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        // ����ʱ��ʱ�����������趨��͸���ȱ仯ʱ��ͬ������ʾ UI ����ʱ��֮��ʱ
        // �˳���Ϸ
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            //��� ����������Ϸ�� bool ����ֵΪ��
            if (doRestart)
            {
                //���¼��ص�ǰָ���ĳ���
                SceneManager.LoadScene(0);
            }
            else
            {
                //�˳���ǰӦ�ó��򣬴������ʱ������Ч
                //Application.Quit();
                // �� Unity �������У�ֹͣ��Ϸ������
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
