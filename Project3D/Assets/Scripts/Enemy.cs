using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerDir = Player.Instance.transform.position - transform.position;
        float angleToPlayer = Mathf.Atan2(-playerDir.z, playerDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angleToPlayer, 0);

        GetComponent<CharacterController>().Move(transform.forward * m_moveSpeed * Time.deltaTime);
    }
}
