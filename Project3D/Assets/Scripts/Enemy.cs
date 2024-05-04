using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask m_playerAttackLayer;
    [SerializeField] float m_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.Instance.transform);

        GetComponent<CharacterController>().Move(transform.forward * m_moveSpeed * Time.deltaTime);

        GetComponent<CharacterController>().Move(Physics.gravity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider) 
    {
        if ((1 << collider.gameObject.layer) == m_playerAttackLayer.value) 
        {
            Debug.Log("Ouch ");
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
