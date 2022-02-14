using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGizmos : MonoBehaviour
{
    [SerializeField] private Transform m_closest = null;
    [SerializeField] private Transform m_closestOnBounds = null;

    private void OnTriggerEnter(Collider other) 
    {
        var closestPoint = other.ClosestPoint(transform.position);
        m_closest.position = closestPoint;
    }
}
