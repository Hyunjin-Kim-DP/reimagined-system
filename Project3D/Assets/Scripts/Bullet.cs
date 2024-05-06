using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject m_particleObject;
    [SerializeField] LayerMask m_excludeLayer;

    bool m_isColiide; // prevent double collide

    float m_lifeTime = 2;

    void Update()
    {
        m_lifeTime -= Time.deltaTime;
        if (m_isColiide == false && m_lifeTime < 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & m_excludeLayer.value) == 0 && m_isColiide == false)
        {
            m_isColiide = true;

            if (m_particleObject != null)
            {
                var obj = Instantiate(m_particleObject, transform.position, transform.rotation);
                Destroy(obj, m_particleObject.GetComponent<ParticleSystem>().main.startLifetime.constant);
            }
            
            Destroy(gameObject);
        }
    }
}
