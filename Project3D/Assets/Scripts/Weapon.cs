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

    [Header("Datas")]
    public int m_maxBullet = 8;
    public int m_bulletCount = 8;    
    [SerializeField] float m_reloadTime = 1;
    bool m_isReloading = false;

    void Start() 
    {
        m_bulletCount = m_maxBullet;
    }

    public void OnShot() 
    {
        if (m_bulletCount > 0 && m_isReloading == false) 
        {
            CreateMuzzleFlash();
            CreateHitBox();
            m_weaponMover.PlayRebound();
            m_bulletCount--;
        }
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

    public void Reload() 
    {
        if (m_isReloading) return;
        StartCoroutine(IEReload());
    }

    IEnumerator IEReload() 
    {   
        m_isReloading = true;
        var barImage = IndicatorCanvas.Instance.m_reloadTimeBar;
        barImage.enabled = true;

        float timer = 0;
        while (timer < m_reloadTime) 
        {
            barImage.fillAmount = timer / m_reloadTime;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        barImage.enabled = false;
        m_bulletCount = m_maxBullet;
        m_isReloading = false;
    }
}
