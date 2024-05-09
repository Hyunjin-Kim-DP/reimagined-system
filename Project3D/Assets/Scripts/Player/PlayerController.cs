using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_speed = 7;
    [SerializeField] float m_jumpImpulse = 6f;
    Vector3 m_moveVelocity;
    [SerializeField] float m_timeToMaxSpeed = 0.1f;
    float smoothInputValue;

    [Space]
    [SerializeField] Transform m_camera;
    [SerializeField] Weapon m_weapon;
    [SerializeField] GroundChecker m_groundChecker;
    [SerializeField] ProceduralSoundPlayer m_footstepPlayer;


    [Space]
    [SerializeField] GameObject m_debugSpawnObject;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            m_weapon.OnShot();
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            GetComponent<Rigidbody>().AddForce(m_camera.forward * m_jumpImpulse, ForceMode.Impulse);
        }
        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            m_weapon.Reload();
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float smoothDelta = 0;
        smoothInputValue = Mathf.SmoothDamp(smoothInputValue, input.normalized.magnitude, ref smoothDelta, m_timeToMaxSpeed);

        Vector2 smoothSpeed = input * m_speed * smoothInputValue * Time.deltaTime;
        m_moveVelocity = transform.forward * smoothSpeed.y + transform.right * smoothSpeed.x;

        if (m_groundChecker.m_isGrounded)
        {
            if (input.magnitude > 0) 
            {
                m_weapon.PlayBobbing();
                m_footstepPlayer.Play();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * m_jumpImpulse, ForceMode.Impulse);
            }
        }

#if DEBUG
        if (Input.GetKeyDown(KeyCode.F) && m_debugSpawnObject != null) 
        {
            Instantiate(m_debugSpawnObject, transform.position + m_camera.forward * 7, Quaternion.identity);
        }
#endif

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + m_moveVelocity);
    }
}
