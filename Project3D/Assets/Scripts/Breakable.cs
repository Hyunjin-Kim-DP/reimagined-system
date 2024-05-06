using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject m_replacement;

    [Space]
    [SerializeField] LayerMask m_breakingLayer;
    [SerializeField] float m_forceToBreak;

    bool m_isBroken;

    void OnCollisionEnter(Collision collision) 
    {
        if (((1 << collision.gameObject.layer) & m_breakingLayer.value) > 0) 
        {
            if (collision.relativeVelocity.magnitude >= m_forceToBreak && m_isBroken == false) 
            {
                m_isBroken = true;

                GameObject broken = Instantiate(m_replacement, transform.position, transform.rotation);
                var rigidbodys = broken.GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rb in rigidbodys) 
                {
                    rb.AddExplosionForce(collision.relativeVelocity.magnitude, collision.contacts[0].point, 2);
                }

                Destroy(gameObject);
            }
        }
    } 
}
