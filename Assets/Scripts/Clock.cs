using UnityEngine;
using System;

/// <summary>
/// The class for the function of an armed clock
/// </summary>
public class Clock : MonoBehaviour
{
    public Transform HoursTransform, MinutesTransform, SecondsTransform; // clock arms
    /// <summary>
    /// hour arm's rotation angle per hour
    /// </summary>
    private const float m_degreesPerHour = 30f;
    /// <summary>
    /// Minite arm's rotation angle per minute
    /// </summary>
    private const float m_degreesPerMinute = 6f;
    /// <summary>
    /// Minite arm's rotation angle per second
    /// </summary>
    private const float m_degreesPerSecond = 6f;

    public bool isContinuous;

    /// <summary>
    /// output some info when clock starts rotation
    /// </summary>
    void Awake()
    {
        Debug.Log("The clock stats at: " + DateTime.Now);
    }

    /// <summary>
    /// Update the clocks according to current time
    /// </summary>FF
    void Update()
    {
        if (isContinuous)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }
    void UpdateDiscrete()
    {
        DateTime currentTime = DateTime.Now;
        HoursTransform.localRotation = Quaternion.Euler(0f, currentTime.Hour * m_degreesPerHour, 0f);
        MinutesTransform.localRotation = Quaternion.Euler(0f, currentTime.Minute * m_degreesPerMinute, 0f);
        SecondsTransform.localRotation = Quaternion.Euler(0f, currentTime.Second * m_degreesPerSecond, 0f);
    }

    void UpdateContinuous()
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        HoursTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalHours * m_degreesPerHour, 0f);
        MinutesTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalMinutes * m_degreesPerMinute, 0f);
        SecondsTransform.localRotation = Quaternion.Euler(0f, (float)currentTime.TotalSeconds * m_degreesPerSecond, 0f);
    }

}