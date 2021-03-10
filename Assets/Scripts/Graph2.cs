using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 函数图形显示逻辑
/// </summary>
public class Graph2 : MonoBehaviour
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
    public int resolution = 10;

    /// <summary>
    /// 所有用于显示的点
    /// </summary>
    Transform[] m_points;

    /// <summary>
    /// 所有的图形显示函数
    /// </summary>
    /// <value>图形显示函数</value>
    readonly static GraphFunction[] functions = {
        SineFunction,
        Sine2DFunction,
        MultipleSineFunction,
        MultipleSine2DFunction,
        Ripple,
        Cylinder,
        Sphere,
        Torus
    };

    /// <summary>
    /// 图形函数名，用于editor中下拉列表选择
    /// </summary>
    public GraphFunctionName functionName;

    /// <summary>
    /// 圆周率近似值
    /// </summary>
    readonly static float Pi = Mathf.PI;

    /// <summary>
    /// 脚本被加载时调用
    /// </summary>
    void Awake()
    {
        // 1. 初始化图形绘制相关参数
        float step = 2.0f / resolution;
        Vector3 scale = Vector3.one * step;
        m_points = new Transform[resolution * resolution];
        // 2. 创建所有点并存储之
        for (int i = 0; i < m_points.Length; i++)
        {
            m_points[i] = Instantiate(PointPrefab);
            m_points[i].localScale = scale;
            m_points[i].SetParent(transform, false);
        }
    }

    /// <summary>
    /// 每帧被调用
    /// </summary>
    void Update()
    {
        // 1. 设定图形绘制的当前参数
        GraphFunction f = functions[(int)functionName];
        float step = 2f / resolution;
        float t = Time.time;
        // 2. 修改所有点的当前位置
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                m_points[i].localPosition = f(u, v, t);
            }
        }
    }

    /// <summary>
    /// 一维输入输出正弦函数
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="z">z坐标（无效）</param>
    /// <param name="t">时间值</param>
    /// <returns>y坐标</returns>
    static Vector3 SineFunction(float x, float z, float t)
    {
        return new Vector3(x, Mathf.Sin((x + t) * Pi), z);
    }
    /// <summary>
    /// 一维输入输出正弦函数（双正弦函数叠加）
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="z">z坐标（无效）</param>
    /// <param name="t">时间值</param>
    /// <returns>y坐标</returns>
    static Vector3 MultipleSineFunction(float x, float z, float t)
    {
        // 1. 根据输入的坐标x计算待显示点的位置并返回
        float y = Mathf.Sin((x + t) * Pi);
        y += Mathf.Sin((x + 2f * t) * 2f * Pi);
        y *= 2f / 3f;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// 二维输入一维输出的正弦函数（双正交一维输入sin函数叠加）
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="z">z坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>y坐标</returns>
    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        // 1. 根据输入的坐标计算待显示点的位置并返回
        return new Vector3(x, (Mathf.Sin(Pi * (x + t)) + Mathf.Sin(Pi * (z + t))) * .5f, z);
    }

    /// <summary>
    /// 二维输入一维输出的正弦函数（多个一维输入sin函数叠加）
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="z">z坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>y坐标</returns>
    static Vector3 MultipleSine2DFunction(float x, float z, float t)
    {
        // 1. 根据输入的坐标和t值计算待显示点的位置
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = 4f * Mathf.Sin(Pi * (x + z + t / 2f));
        p.y += Mathf.Sin(Pi * (x + t));
        p.y += Mathf.Sin(2f * Pi * (z + 2f * t)) * .5f;
        return new Vector3(x, p.y * 1f / 5.5f, z);
    }

    /// <summary>
    /// 波纹函数
    /// </summary>
    /// <param name="x">x坐标</param>
    /// <param name="z">z坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>y坐标</returns>
    static Vector3 Ripple(float x, float z, float t)
    {
        // 1. 根据输入的坐标和t值计算待显示点的位置并返回
        float dis = Mathf.Sqrt(x * x + z * z);
        return new Vector3(x, Mathf.Sin(4 * Pi * dis - 4 * t) / (10 * dis * dis + 5), z);
    }

    /// <summary>
    /// 柱面函数
    /// </summary>
    /// <param name="u">u坐标</param>
    /// <param name="v">v坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>点坐标</returns>
    static Vector3 Cylinder(float u, float v, float t)
    {
        // 1. 根据输入的u、v、t值计算待显示点的位置并返回
        Vector3 p;
        float r = 0.8f + Mathf.Sin(Pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(u * Pi);
        p.y = v;
        p.z = r * Mathf.Cos(u * Pi);
        return p;
    }

    /// <summary>
    /// 球函数
    /// </summary>
    /// <param name="u">u坐标</param>
    /// <param name="v">v坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>点坐标</returns>
    static Vector3 Sphere(float u, float v, float t) // 其实是个极坐标+一根直角坐标，直角坐标的粒度也是变动的
    {
        // 1. 根据输入的u、v、t值计算待显示点的位置并返回
        Vector3 p;
        float r = 0.8f + Mathf.Sin(Pi * (6f * u + t)) * .1f;
        r += Mathf.Sin(Pi * (4f * v + t)) * 0.1f; // 如果想退化成球，使用固定值即可
        float s = r * Mathf.Cos(Pi * 0.5f * v);
        p.x = s * Mathf.Sin(u * Pi);
        p.y = r * Mathf.Sin(Pi * 0.5f * v);
        p.z = s * Mathf.Cos(u * Pi);
        return p;
    }

    /// <summary>
    /// 圆环面函数
    /// </summary>
    /// <param name="u">u坐标</param>
    /// <param name="v">v坐标</param>
    /// <param name="t">时间值</param>
    /// <returns>点坐标</returns>
    static Vector3 Torus(float u, float v, float t)
    {
        // 1. 根据输入的u、v、t值计算待显示点的位置并返回
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(Pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(Pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(Pi * v) + r1;
        p.x = s * Mathf.Sin(u * Pi);
        p.y = r2 * Mathf.Sin(Pi * v);
        p.z = s * Mathf.Cos(u * Pi);
        return p;
    }
}
