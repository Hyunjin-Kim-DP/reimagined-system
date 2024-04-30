using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorCanvas : MonoBehaviour
{
    [SerializeField] Player m_player;
    [SerializeField] Text m_playerHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_playerHP.text = m_player.m_hp.ToString();
    }
}
