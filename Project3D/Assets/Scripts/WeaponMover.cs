using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMover : MonoBehaviour
{
    Transform m_weapon;

    [Header("Sway")] 
    [SerializeField] Vector2 m_swayPosition;
    [SerializeField] Vector2 m_swayThreshold = new Vector2(0.2f, 0.1f);
    [SerializeField] float m_swaySmooth = 2.5f;
    [SerializeField] float m_swayReturnSmooth = 5f;

    [Space]
    [SerializeField] Vector3 m_swayEulerAngle;
    [SerializeField] Vector3 m_swayEulerThreshold = new Vector3(30, 15, 80);
    [SerializeField] float m_swayAngleSmooth = 5;
    [SerializeField] float m_swayAngleReturnSmooth = 7;

    [Header("Bobbing")]
    [SerializeField] float m_bobbingFrequency = 10;
    [SerializeField] float m_bobbingAmplitude = .1f;
    [SerializeField] float m_bobbingSmooth = 10;
    Vector3 m_originLocalPos;
    float m_bobbingTimer = 0;

    [Header("Rebound")]
    [SerializeField] float m_power = 80;
    [SerializeField] float m_reboundSmooth = 30;
    [SerializeField] float m_reboundReturnSmooth = 10;
    Vector3 m_reboundEuler;

    // Start is called before the first frame update
    void Start()
    {
        m_weapon = transform.GetChild(0);
        m_originLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {   
        m_weapon.localRotation = Quaternion.Slerp(m_weapon.localRotation, Quaternion.Euler(m_swayEulerAngle), Time.deltaTime * m_swayAngleSmooth); 

        m_swayEulerAngle.x = Mathf.Clamp(m_swayEulerAngle.x + Input.GetAxis("Mouse Y"), -m_swayEulerThreshold.x, m_swayEulerThreshold.x);
        m_swayEulerAngle.y = Mathf.Clamp(m_swayEulerAngle.z + Input.GetAxis("Mouse X"), -m_swayEulerThreshold.y, m_swayEulerThreshold.y);
        m_swayEulerAngle.z = Mathf.Clamp(m_swayEulerAngle.z + Input.GetAxis("Mouse X"), -m_swayEulerThreshold.z, m_swayEulerThreshold.z);

        m_swayEulerAngle = Vector3.Lerp(m_swayEulerAngle, Vector3.zero, Time.deltaTime * m_swayAngleReturnSmooth);

        m_weapon.localPosition = Vector3.Lerp(m_weapon.localPosition, m_swayPosition, Time.deltaTime * m_swaySmooth);
        
        m_swayPosition.x = Mathf.Clamp(m_swayPosition.x + Input.GetAxis("Mouse X"), -m_swayThreshold.x, m_swayThreshold.x);
        m_swayPosition.y = Mathf.Clamp(m_swayPosition.y + Input.GetAxis("Mouse Y"), -m_swayThreshold.y, m_swayThreshold.y);

        m_swayPosition = Vector3.Lerp(m_swayPosition, Vector3.zero, Time.deltaTime * m_swayReturnSmooth);

        transform.localPosition = Vector3.Lerp(transform.localPosition, m_originLocalPos, Time.deltaTime * m_bobbingSmooth);

        m_reboundEuler = Vector3.Lerp(m_reboundEuler, Vector3.zero, Time.deltaTime * m_reboundReturnSmooth);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(m_reboundEuler), Time.deltaTime * m_reboundSmooth);
    }

    public void UpdateBobbing() 
    {
        m_bobbingTimer += Time.deltaTime * m_bobbingFrequency;
        var bobbingLocal = transform.localPosition + Vector3.up * Mathf.Sin(m_bobbingTimer) * m_bobbingAmplitude;
        transform.localPosition = Vector3.Lerp(transform.localPosition, bobbingLocal, Time.deltaTime * m_bobbingSmooth);
    }

    public void PlayRebound() => m_reboundEuler.x = -m_power;
}
