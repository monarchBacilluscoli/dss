using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoursIndicator : MonoBehaviour
{
    /// <summary>
    /// use to store the original color
    /// </summary>
    public Color m_initialColor;
    /// <summary>
    /// default active color
    /// </summary>
    public Color m_activeColor = new Color(.6f, .2f, .2f);

    /// <summary>
    /// Initialize the data
    /// </summary>
    void Start()
    {
        m_initialColor = GetComponent<MeshRenderer>().material.color;
    }

    /// <summary>
    /// Triggered when other collider enter
    /// </summary>
    /// <param name="collider">The collider collided with this</param>
    void OnTriggerEnter(Collider collider)
    {
        // 1. Change the material's color when it is collided by the arm
        if (collider.gameObject.name == "Extended Collider")
        {
            GetComponent<MeshRenderer>().material.color = m_activeColor;
        }
    }

    /// <summary>
    /// Triggered when other collider exit
    /// </summary>
    /// <param name="collider">The collider collided with this</param>
    void OnTriggerExit(Collider collider)
    {
        // 1. Reset the material's color when the second arm exit
        if (collider.gameObject.name == "Extended Collider")
        {
            GetComponent<MeshRenderer>().material.color = m_initialColor;
        }
    }
}
