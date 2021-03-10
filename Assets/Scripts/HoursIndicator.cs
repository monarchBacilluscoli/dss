using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoursIndicator : MonoBehaviour
{
    /// <summary>
    /// 存储indicator初始材质颜色
    /// </summary>
    public Color m_initialColor;
    /// <summary>
    /// 默认indicator的激活颜色
    /// </summary>
    public Color m_activeColor = new Color(.6f, .2f, .2f);

    /// <summary>
    /// 在脚本enable的时候调用
    /// </summary>
    /// <remarks>
    /// <para>后于所有物体的<c>Awake</c>函数之后调用，可用于处理初始化依赖</para>
    /// <para>先于<c>Update</c>函数之前调用</para>
    /// </remarks>
    void Start()
    {
        m_initialColor = GetComponent<MeshRenderer>().material.color;
    }

    /// <summary>
    /// 当触发器碰撞发生时被调用
    /// </summary>
    /// <param name="collider">与之发生碰撞的物体</param>
    /// <remarks>
    /// <para>1. 两个对象都要有<c>Collider</c></para>
    /// <para>2. 一方需要<c>Collider.isTrigger on</c>以及<c>Rigidbody</c></para>
    /// <para>3. 如果两者都<c>Collider.isTrigger on</c>，则无效（似乎触发器允许叠加）</para>
    /// </remarks>
    void OnTriggerEnter(Collider collider)
    {
        // 1. 如果进入碰撞对象名为“Extended Collider”
        if (collider.gameObject.name == "Extended Collider")
        {
            // 2.CompareTo 修改该物体的颜色
            GetComponent<MeshRenderer>().material.color = m_activeColor;
        }
    }

    /// <summary>
    /// 当触发器碰撞结束时被调用
    /// </summary>
    /// <param name="collider">与之发生碰撞的物体</param>
    void OnTriggerExit(Collider collider)
    {
        // 1. 如果离去碰撞对象名为“Extended Collider”
        if (collider.gameObject.name == "Extended Collider")
        {
            // 2. 重置物体的颜色
            GetComponent<MeshRenderer>().material.color = m_initialColor;
        }
    }
}
