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
        if (HP <= 0) return;

        var diff = Player.Instance.transform.position - transform.position;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, -(Mathf.Atan2(diff.z, diff.x) + Mathf.PI * .5f) * Mathf.Rad2Deg, 0));
    }

    void FixedUpdate() 
    {
        if (HP <= 0) return;

        GetComponent<Rigidbody>().MovePosition(transform.position + -transform.forward * m_moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) 
    {
        if ((1 << collision.gameObject.layer) == m_playerAttackLayer.value) 
        {
            if (HP > 0) 
            {
                HP--;
                if (HP <= 0) 
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    GetComponent<Rigidbody>().velocity = Vector3.zero; 

                    var obj = Instantiate(m_particleObject, transform.position, transform.rotation);
                    obj.GetComponent<ParticleSystemRenderer>().material = Instantiate(GetComponent<MeshRenderer>().material);
                    Destroy(obj, obj.GetComponent<ParticleSystem>().main.startLifetime.constant);

                    Destroy(gameObject);
                }
            }
        }
    }
}
