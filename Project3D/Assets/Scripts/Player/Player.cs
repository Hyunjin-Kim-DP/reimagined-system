using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] PlayerController m_pController;

    [SerializeField] LayerMask m_enemyLayer;

    [Space]
    public float m_maxHp = 100;
    public float m_hp;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        m_hp = m_maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision) 
    {
        if ((1 << collision.gameObject.layer) == m_enemyLayer.value) 
        {
            m_hp -= 7;
            if (m_hp < 0) 
            {
                m_hp = 0;
                m_pController.enabled = false;
            }
        }
    }
}
