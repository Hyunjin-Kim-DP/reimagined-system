using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] LayerMask m_groundLayer;

    public bool m_isGrounded = false;

    void OnTriggerEnter(Collider collider) 
    {
        if ((1 << collider.gameObject.layer) == m_groundLayer.value) 
        {
            m_isGrounded = true;
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        if ((1 << collider.gameObject.layer) == m_groundLayer.value) 
        {
            m_isGrounded = false;
        }
    }
}
