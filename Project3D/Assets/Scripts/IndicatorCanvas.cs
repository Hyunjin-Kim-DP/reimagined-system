using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorCanvas : MonoBehaviour
{
    public static IndicatorCanvas Instance { get; private set; }

    [SerializeField] Player m_player;
    [SerializeField] Text m_playerHP;

    [Space]
    [SerializeField] Weapon m_weapon;
    [SerializeField] Text m_weaponBullet;
    public Image m_reloadTimeBar;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_reloadTimeBar.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerHP.text = m_player.m_hp.ToString();
        m_weaponBullet.text = m_weapon.m_bulletCount.ToString() + " / " + m_weapon.m_maxBullet.ToString();
    }
}
