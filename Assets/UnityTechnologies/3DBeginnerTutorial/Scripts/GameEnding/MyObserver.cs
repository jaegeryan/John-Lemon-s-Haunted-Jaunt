using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来通过触发器代表的敌人视线监视玩家的代码
public class MyObserver : MonoBehaviour
{
    //声明共有的玩家变换组件对象，用来挂接玩家
    public Transform player;
    //声明一个bool变量，表示玩家是否被视线扫到
    //即是否进入代表敌人视线的触发器区域内
    bool m_IsPlayerInRange;
    // 声明游戏结束脚本组件类对象，为了调用
    // 游戏结束脚本中的公有方法（函数）
    //public GameEnding gameEnding;
    public MyGameEnding myGameEnding;

    //进入触发器事件
    //玩家进入视线触发器区域，
    //更改开关值
    private void OnTriggerEnter(Collider other)
    {
        //如果进入触发器区域的是玩家
        if(other.transform == player)
        {
            //开关变量值设置为真
            m_IsPlayerInRange = true;  
        }
    }

    //离开触发器事件
    private void OnTriggerExit(Collider other)
    {
        //当触发器区域中的是玩家
        if (other.transform == player) {
            //离开时，设置开关值为假
            m_IsPlayerInRange = false;
        }
    }

    //在 Update 中监控开关的值，一旦玩家进入视线触发器区域
    //执行对应的逻辑
    private void Update()
    {
        //触发器已被触发
        if (m_IsPlayerInRange)
        {
            //设置投射射线用到的方向矢量
            Vector3 direction = player.position - transform.position + Vector3.up;
            //创建射线
            Ray ray = new Ray(transform.position, direction);

            //射线击中对象，包含射线碰撞信息
            RaycastHit raycastHit;

            //使用物理系统发射射线，如果碰到物体
            // 进入第一层 if
            // out 代表第二个参数是输出参数，可以带出数据到参数中
            if (Physics.Raycast(ray, out raycastHit))
            {
                //如果碰到的是玩家
                if (raycastHit.collider.transform == player)
                {
                    //调用 GameEnding 脚本中的抓到玩家的方法
                    myGameEnding.CaughtPlayer();
                }
            }
        }
        
    }

}
