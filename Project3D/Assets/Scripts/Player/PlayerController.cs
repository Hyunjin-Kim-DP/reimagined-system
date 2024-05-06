using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon m_weapon;

    [Space]    
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] Transform m_groundChecker;
    bool m_isGrounded;

    [Space]
    [SerializeField] float m_speed = 7;
    [SerializeField] float m_maxSpeed = 20f;
    [SerializeField] float m_jumpHeight = 2.5f;

    Vector3 velocity;

    [Space]
    [SerializeField] ProceduralSoundPlayer m_footstepPlayer;

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

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            m_weapon.Reload();
        }

        Vector3 forward = transform.forward * Input.GetAxis("Vertical") * m_speed;
        Vector3 right = transform.right * Input.GetAxis("Horizontal") * m_speed;
        GetComponent<CharacterController>().Move((forward + right) * Time.deltaTime);

        m_isGrounded = Physics.BoxCast(m_groundChecker.position, Vector3.one * .5f, Vector3.down, 
            Quaternion.identity, .1f, m_groundLayer);
        if (m_isGrounded)
        {
            if ((forward + right).magnitude > m_speed * 0.5f) 
            {
                m_weapon.PlayBobbing();
                m_footstepPlayer.Play();
            }

            velocity.y = Mathf.Clamp(velocity.y, Physics.gravity.y, m_maxSpeed);
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * Physics.gravity.y);
            }
        }

        velocity.y = Mathf.Clamp(velocity.y + Physics.gravity.y * Time.deltaTime, Physics.gravity.y, m_maxSpeed);
        GetComponent<CharacterController>().Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos() 
    {
        if (m_groundChecker == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(m_groundChecker.position, Vector3.one);
    }
}