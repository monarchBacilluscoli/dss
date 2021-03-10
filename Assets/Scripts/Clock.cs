using UnityEngine;
using System;

/// <summary>
/// 钟表运行逻辑
/// </summary>
public class Clock : MonoBehaviour
{
    public Transform HoursTransform, MinutesTransform, SecondsTransform; // clock arms
    /// <summary>
    /// 时针每小时转动的角度
    /// </summary>
    private const float m_degreesPerHour = 30f;
    /// <summary>
    /// 分针每分钟转动的角度
    /// </summary>
    private const float m_degreesPerMinute = 6f;
    /// <summary>
    /// 秒针每秒转动的角度
    /// </summary>
    private const float m_degreesPerSecond = 6f;

    /// <summary>
    /// 表针是否连续移动
    /// </summary>
    public bool isContinuous;

    /// <summary>
    /// <para>脚本被加载时调用:</para>
    /// <para>1. 当挂载脚本的Active GameObject在场景加载时被初始化的时候</para>
    /// <para>2. 当GameObject从inactive到active的时候</para>
    /// <para>3. 被Instantiate的时候</para>
    /// </summary>
    void Awake()
    {
        // 1. log输出启动时间
        Debug.Log("The clock stats at: " + DateTime.Now);
    }

    /// <summary>
    /// 每帧被调用
    /// </summary>
    void Update()
    {
        // 1. 根据isContinuous来判断调用连续移动函数还是非连续移动函数
        if (isContinuous)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }
    /// <summary>
    /// 非连续移动表针函数
    /// </summary>
    void UpdateDiscrete()
    {
        // 1. 使用分立的时间表示（DataTime）来表示当前时间并设置表针位置
        DateTime currentTime = DateTime.Now;
        HoursTransform.localRotation = Quaternion.Euler(0f, currentTime.Hour * m_degreesPerHour, 0f);
        MinutesTransform.localRotation = Quaternion.Euler(0f, currentTime.Minute * m_degreesPerMinute, 0f);
        SecondsTransform.localRotation = Quaternion.Euler(0f, currentTime.Second * m_degreesPerSecond, 0f);
    }

    /// <summary>
    /// 连续移动表针函数
    /// </summary>
    void UpdateContinuous()
    {
        // 1. 使用连续的时间表示（TimeOfDay）来表示当前时间并设置表针位置
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        HoursTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalHours * m_degreesPerHour, 0f);
        MinutesTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalMinutes * m_degreesPerMinute, 0f);
        SecondsTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalSeconds * m_degreesPerSecond, 0f);
    }

}