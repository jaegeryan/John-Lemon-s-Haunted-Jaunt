using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;

    //创建变量，获取用户输入方向
    float horizontal;
    float vertical;
    //四元素是用来表示旋转的，这里用来表示玩家的旋转；初始化为不旋转
    Quaternion m_Rotation = Quaternion.identity;
    
    public float turnSpeed = 20f;

    //创建刚体组件,用来移动玩家
    Rigidbody m_Rigidbody;
    Animator m_animator;

    void Start()
    {
        //在游戏开始时获取刚体组件和动画组件
        m_Rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    //用fixedUpdate是因为在Update中移动玩家会有延迟而且会有抖动
    private void FixedUpdate()
    {
        //将用户输入的方向赋值给m_Movement,并且归一化
        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();
        
        //判断是否有横向移动
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        //判断是否有纵向移动
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        //判断是否有移动
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_animator.SetBool("isWalking", isWalking);
        //如果有移动，就将玩家的旋转设置为移动方向
        if (isWalking)
        {
            //将玩家的旋转设置为移动方向
            //desiredForward是玩家当前的旋转方向，m_Movement是玩家的移动方向，180 * Time.deltaTime是旋转速度，0f是旋转的最小值
            //Vector3.RotateTowards是将玩家当前的旋转方向旋转到玩家的移动方向,transform.forward是玩家当前的旋转方向是从Unity中获取的
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            //将玩家的旋转设置为移动方向
            m_Rotation = Quaternion.LookRotation(desiredForward);
        }
    }
    
    //OnAnimatorMove是在动画移动时调用的,是内置的方法，只有在动画移动时才会调用
    private void OnAnimatorMove()
    {
        //使用用户输入的三维向量作为移动方向从而移动玩家，deltaPosition.magnitude是动画的移动距离(0.02秒移动的距离)
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}