using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponMover m_weaponMover;

    [SerializeField] GameObject m_muzzleFlashPrefab;
    [SerializeField] Transform m_camera;
    [SerializeField] Transform m_muzzleFlashPosition;

    [Space]
    [SerializeField] GameObject m_hitBoxPrefab;
    [SerializeField] float m_hitboxDistance = 1.5f;

    public void OnShot() 
    {
        CreateMuzzleFlash();
        CreateHitBox();
        m_weaponMover.PlayRebound();
    }

    void CreateMuzzleFlash() 
    {
        var muzzleFlashObject = Instantiate(m_muzzleFlashPrefab, m_camera); 
        muzzleFlashObject.transform.position = m_muzzleFlashPosition.position;
        muzzleFlashObject.transform.rotation = Quaternion.Euler(m_camera.eulerAngles.x, m_camera.eulerAngles.y, UnityEngine.Random.value * 360);       
        Destroy(muzzleFlashObject, 0.1f);
    }

    void CreateHitBox() 
    {
        var hitBoxObject = Instantiate(m_hitBoxPrefab);
        hitBoxObject.transform.position = m_camera.position + m_camera.forward * m_hitboxDistance;
        hitBoxObject.transform.rotation = Quaternion.Euler(m_camera.eulerAngles.x, m_camera.eulerAngles.y, 0);
        Destroy(hitBoxObject, 0.1f);
    }

    public void PlayBobbing() => m_weaponMover.UpdateBobbing();
}
