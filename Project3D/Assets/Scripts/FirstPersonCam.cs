using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] Transform m_follow;

    Vector2 m_inputVector;
    
    [Space]
    [SerializeField] float m_verticalAngleLimit = 70;
    
    public float m_sensetivity = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_inputVector.x += Input.GetAxis("Mouse X") * m_sensetivity;
        m_inputVector.y += -Input.GetAxis("Mouse Y") * m_sensetivity;
        m_inputVector.y = Mathf.Clamp(m_inputVector.y, -90, m_verticalAngleLimit);

        transform.rotation = Quaternion.Euler(m_inputVector.y, m_inputVector.x, 0);
        transform.position = m_follow.position;

        m_player.rotation = Quaternion.Euler(0, m_inputVector.x, 0);
    }
}
