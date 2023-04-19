using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameEnding : MonoBehaviour
{
    // 声明一个开关变量，存储用户是否在触发器中
    bool m_IsPlayerAtExit;
    // 声明一个公开对象，用来获取用户角色
    public GameObject player;

    // 更改 透明度的时间
    public float fadeDuration = 1.0f;
    // 计时器
    float m_Timer;
    // 正常（完全不透明状态）显示结束 UI 的时间
    public float displayImageDuration = 1.0f;

    // 声明一个 CanvasGroup , 用来获取 UI 中的实例，来更改 UI 中图像的透明度
    public CanvasGroup exitBackgroundImageCanvasGroup;
    //显示结束图片的组件对象
    public Image image;


    //  用一个bool 变量代表玩家是否被敌人抓到
    bool m_IsPlayerCaught;
    // 创建两个对象，分别表示从硬盘中获取到的图片素材
    Sprite spriteCaught;
    Sprite spriteWon;
    private void Start()
    {
        // 使用 Resource.Load 方法，可以直接读取项目文件夹 Assets/Resources 中的文件
        // 以文件名为参数
        spriteCaught = Resources.Load<Sprite>("Caught");
        spriteWon = Resources.Load<Sprite>("Won");
    }

    //如果被抓到，设置开关值为真
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
    //触发器事件
    private void OnTriggerEnter(Collider other)
    {
        // 如果和触发器碰撞（进入触发器）的是玩家对象
        if (other.gameObject == player)
        {
            //将开关设置为 true
            m_IsPlayerAtExit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 如果用户进入结束区域
        if (m_IsPlayerAtExit)
        {
            //调用结束游戏方法
            EndLevel(spriteWon, false);
        }
        //如果玩家被抓到
        else if (m_IsPlayerCaught)
        {
            //调用结束游戏的方法
            EndLevel(spriteCaught, true);
        }
    }

    // 结束当前关卡，当前游戏只有一个 scene ，就相当于结束游戏
    // 参数1：结束UI对应的游戏对象
    // 参数2：是否重新开始
    void EndLevel(Sprite sprite, bool doRestart)
    {
        // 计时器随着 Update 逐渐增大
        m_Timer += Time.deltaTime;
        //指定结束UI Image 组件中显示的图片源
        image.sprite = sprite;
        // 逐渐更改透明度
        // 透明度 alpha 从 0 到1，从完全透明到完全不透明，大于1 以后就是完全不透明
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        // 当计时器时长大于我们设定的透明度变化时长同正常显示 UI 界面时长之和时
        // 退出游戏
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            //如果 代表重启游戏的 bool 变量值为真
            if (doRestart)
            {
                //重新加载当前指定的场景
                SceneManager.LoadScene(0);
            }
            else
            {
                //退出当前应用程序，打包发布时才能生效
                //Application.Quit();
                // 在 Unity 编译器中，停止游戏的运行
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
