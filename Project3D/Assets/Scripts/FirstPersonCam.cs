using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] Transform m_player;
    Vector2 m_inputVector;

    [Space]
    [SerializeField] float m_verticalAngleLimit = 70;
    
    public float m_sensetivity = 30;

    [Header("Weapon")]
    [SerializeField] Transform m_weapon;

    [Space]
    [SerializeField] float m_swayPosStep = .01f;
    [SerializeField] float m_swayPosMaxStep = .06f;
    [SerializeField] Vector3 m_swayPosition;
    [SerializeField] float m_smoothPosition = 10;

    [Space]
    [SerializeField] float m_swayRotStep = 4f;
    [SerializeField] float m_swayRotMaxStep = 5f;
    Vector3 m_swayEulerRotation;
    [SerializeField] float m_smoothRotation = 14;

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
        m_player.rotation = Quaternion.Euler(0, m_inputVector.x, 0);

        Debug.Log(m_inputVector);

        m_weapon.localPosition = Vector3.Lerp(m_weapon.localPosition, m_swayPosition, Time.deltaTime * m_smoothPosition);
    }

    void UpdateWeaponSway() 
    {
        SwayWeaponPosition();
        SwayWeaponRotation();

        m_weapon.localPosition = Vector3.Lerp(m_weapon.localPosition, m_swayPosition, Time.deltaTime * m_smoothPosition);

        m_weapon.localRotation = Quaternion.Slerp(m_weapon.localRotation, 
            Quaternion.Euler(m_swayEulerRotation), Time.deltaTime * m_smoothRotation);
    }

    void SwayWeaponPosition() 
    {
        Vector3 invertLook = m_inputVector * -m_swayPosStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -m_swayPosMaxStep, m_swayPosMaxStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -m_swayPosMaxStep, m_swayPosMaxStep);

        m_swayPosition = invertLook;
    }

    void SwayWeaponRotation() 
    {
        var invertLook = m_inputVector * -m_swayRotStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -m_swayRotMaxStep, m_swayRotMaxStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -m_swayRotMaxStep, m_swayRotMaxStep);

        m_swayEulerRotation = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }
}
