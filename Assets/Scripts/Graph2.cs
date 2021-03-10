using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class is used as the script of the Graph Object in Mathmaticl Surfaces scene to display function graph
/// </summary>
public class Graph2 : MonoBehaviour
{
    /// <summary>
    /// The prefab used to display a point on a funcion
    /// </summary>
    public Transform PointPrefab;

    /// <summary>
    /// The number of the points
    /// </summary>
    [Range(10, 100)]
    public int resolution = 10;

    /// <summary>
    /// All the points to display a funcion
    /// </summary>
    Transform[] points;

    static GraphFunction[] functions = {
        SineFunction,
        Sine2DFunction,
        MultipleSineFunction,
        MultipleSine2DFunction,
        Ripple,
        Cylinder,
        Sphere,
        Torus
    };

    public GraphFunctionName functionName;

    const float Pi = Mathf.PI;

    /// <summary>
    /// Triggered when awake
    /// </summary>
    void Awake()
    {
        // 1. Initialize all function points and store them
        float step = 2.0f / resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(PointPrefab);
            points[i].localScale = scale;
            points[i].SetParent(transform, false);
        }
    }

    /// <summary>
    /// Triggered at each frame
    /// </summary>
    void Update()
    {
        // 1. update function points position
        GraphFunction f = functions[(int)functionName];
        float step = 2f / resolution;
        float t = Time.time;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }

    /// <summary>
    /// Sine function
    /// </summary>
    /// <param name="x">input x</param>
    /// <param name="t">input time</param>
    /// <returns>the calculation result</returns>
    static Vector3 SineFunction(float x, float z, float t)
    {
        return new Vector3(x, Mathf.Sin((x + t) * Pi), z);
    }
    /// <summary>
    /// 2 sine functions overlay
    /// </summary>
    /// <param name="x">input x</param>
    /// <param name="t">input time</param>
    /// <returns>the calculation result</returns>
    static Vector3 MultipleSineFunction(float x, float z, float t)
    {
        float y = Mathf.Sin((x + t) * Pi);
        y += Mathf.Sin((x + 2f * t) * 2f * Pi);
        y *= 2f / 3f;
        return new Vector3(x, y, z);
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        return new Vector3(x, (Mathf.Sin(Pi * (x + t)) + Mathf.Sin(Pi * (z + t))) * .5f, z);
    }

    static Vector3 MultipleSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.z = z;
        p.y = 4f * Mathf.Sin(Pi * (x + z + t / 2f));
        p.y += Mathf.Sin(Pi * (x + t));
        p.y += Mathf.Sin(2f * Pi * (z + 2f * t)) * .5f;
        return new Vector3(x, p.y * 1f / 5.5f, z);
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        float dis = Mathf.Sqrt(x * x + z * z);
        return new Vector3(x, Mathf.Sin(4 * Pi * dis - 4 * t) / (10 * dis * dis + 5), z);
    }

    static Vector3 Cylinder(float theta, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(Pi * (6f * theta + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(theta * Pi);
        p.y = v;
        p.z = r * Mathf.Cos(theta * Pi);
        return p;
    }

    static Vector3 Sphere(float theta, float v, float t) // 其实是个极坐标+一根直角坐标，直角坐标的粒度也是变动的
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(Pi * (6f * theta + t)) * .1f;
        r += Mathf.Sin(Pi * (4f * v + t)) * 0.1f; // 如果想退化成球，使用固定值即可
        float s = r * Mathf.Cos(Pi * 0.5f * v);
        p.x = s * Mathf.Sin(theta * Pi);
        p.y = r * Mathf.Sin(Pi * 0.5f * v);
        p.z = s * Mathf.Cos(theta * Pi);
        return p;
    }

    static Vector3 Torus(float theta, float v, float t)
    {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(Pi * (6f * theta + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(Pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(Pi * v) + r1;
        p.x = s * Mathf.Sin(theta * Pi);
        p.y = r2 * Mathf.Sin(Pi * v);
        p.z = s * Mathf.Cos(theta * Pi);
        return p;
    }
}
