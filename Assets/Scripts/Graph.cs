using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Graph : MonoBehaviour
{
    /// <summary>
    /// The prefab used to display a point on a funcion
    /// </summary>
    public Transform PointPrefab;

    /// <summary>
    /// The distance from start to end position of the x
    /// </summary>
    public float m_range = 2f;

    /// <summary>
    /// The number of the points
    /// </summary>
    [Range(10, 100)]
    public int m_resolution = 10;

    /// <summary>
    /// All the points to display a funcion
    /// </summary>
    Transform[] points;

    /// <summary>
    /// whether to display a full plane
    /// </summary>
    public bool m_isDisplayFullDomain;

    /// <summary>
    /// Triggered when awake
    /// </summary>
    void Awake()
    {
        // 1. Display a full plane
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
        // 2. Initialize all function points and store them
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
    /// Triggered at each frame
    /// </summary>
    void Update()
    {
        // 1. update function points
        float step = 2.0f / m_resolution;
        Vector3 position;
        position.z = 0;
        for (int i = 0; i < points.Length; i++)
        {
            position.x = (i + 0.5f) * step - 1f;
            position.y = Mathf.Sin((position.x + Time.time) * Mathf.PI);
            points[i].transform.position = position;
        }
    }
}
