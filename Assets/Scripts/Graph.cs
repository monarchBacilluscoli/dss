using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 函数图形显示逻辑
/// </summary>
public class Graph : MonoBehaviour
{
    /// <summary>
    /// 用于表示点的预制体
    /// </summary>
    public Transform PointPrefab;

    /// <summary>
    /// 点的分辨率
    /// </summary>
    /// <remarks>
    /// 越大点越密
    /// </remarks>
    [Range(10, 100)]
    public int m_resolution = 10;

    /// <summary>
    /// 所有用于显示的点
    /// </summary>
    Transform[] points;

    /// <summary>
    /// 脚本被加载时调用
    /// </summary>
    void Awake()
    {
        // 1. 显示一整个面（用于查看shader的效果）
        float step = 2.0f / m_resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 3;
        for (int i = 0; i < m_resolution; i++)
        {
            for (int j = 0; j < m_resolution; j++)
            {
                position.x = (i + 0.5f) * step - 1f;
                position.y = (j + 0.5f) * step - 1f;
                Transform point = Instantiate(PointPrefab, position, Quaternion.identity); // transform is the Object's transform
                point.SetParent(transform, false);
                point.localScale = scale;
            }
        }
        // 2. 初始化所有点并存储之
        position = Vector3.zero;
        points = new Transform[m_resolution];
        for (int i = 0; i < points.Length; i++)
        {
            position.x = (i + 0.5f) * step - 1f;
            points[i] = Instantiate(PointPrefab, position, Quaternion.identity);
            points[i].localScale = scale;
            points[i].SetParent(transform, false);
        }
    }

    /// <summary>
    /// 每帧被调用
    /// </summary>
    void Update()
    {
        // 1. 设定图形绘制的当前参数
        float step = 2.0f / m_resolution;
        Vector3 position;
        position.z = 0;
        // 2. 修改点的当前位置
        for (int i = 0; i < points.Length; i++)
        {
            position.x = (i + 0.5f) * step - 1f;
            position.y = Mathf.Sin((position.x + Time.time) * Mathf.PI);
            points[i].transform.position = position;
        }
    }
}
