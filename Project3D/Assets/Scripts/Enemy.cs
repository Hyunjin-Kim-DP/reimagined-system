using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask m_playerAttackLayer;
    [SerializeField] float m_moveSpeed;
    [SerializeField] int HP = 3;

    [Space]
    [SerializeField] GameObject m_particleObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Player.Instance.transform);
        var diff = Player.Instance.transform.position - transform.position;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, -(Mathf.Atan2(diff.z, diff.x) + Mathf.PI * .5f) * Mathf.Rad2Deg, 0));
    }

    void FixedUpdate() 
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + -transform.forward * m_moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider) 
    {
        if ((1 << collider.gameObject.layer) == m_playerAttackLayer.value) 
        {
            if (HP > 0) 
            {
                HP--;
                if (HP <= 0) 
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                    var obj = Instantiate(m_particleObject, transform.position, transform.rotation);
                    Destroy(obj, obj.GetComponent<ParticleSystem>().main.startLifetime.constant);
                }
            }
        }
    }
}
