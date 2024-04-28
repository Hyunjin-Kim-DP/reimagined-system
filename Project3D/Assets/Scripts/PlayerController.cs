using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask m_groundMask;
    bool m_isGrounded;
    
    [Space]
    [SerializeField] float m_speed = 10;
    [SerializeField] float m_maxSpeed = 20f;
    [SerializeField] float m_jumpImpulse = 5;

    [Space]
    [SerializeField] float m_groundDrag = 5;
    [SerializeField] float m_airMoveFactor = .3f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        var moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        Vector3 moveVector = m_isGrounded ? moveDirection.normalized * m_speed : moveDirection.normalized * m_speed * m_airMoveFactor;
        
        GetComponent<Rigidbody>().AddForce(moveVector);
        UpdateSpeedLimit(moveDirection);

        m_isGrounded = Physics.BoxCast(transform.position, Vector3.one * .5f, Vector3.down, Quaternion.identity, 
            GetComponent<CapsuleCollider>().height * .5f, m_groundMask);

        GetComponent<Rigidbody>().drag = m_isGrounded ? m_groundDrag : 0;

        if (m_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * m_jumpImpulse, ForceMode.Impulse);
        }
    }

    void UpdateSpeedLimit(Vector3 moveDir) 
    {
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        velocity.y = 0;

        if (velocity.magnitude > m_maxSpeed) 
        {
            Vector3 clamped = velocity.normalized * m_maxSpeed;
            GetComponent<Rigidbody>().velocity = new Vector3(clamped.x, GetComponent<Rigidbody>().velocity.y, clamped.z);
        }
    }
}
