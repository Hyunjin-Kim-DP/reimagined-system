using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] m_SFXList;
    int m_index = 0;

    int m_randomIndex => UnityEngine.Random.Range(0, m_SFXList.Length);

    [SerializeField] bool m_changeOnTime;
    [SerializeField] bool m_isRandom;
    [SerializeField] float m_soundInterval = .3f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (m_isRandom) m_index = m_randomIndex;
    }

    public void Play() 
    {
        if (m_changeOnTime) 
        {
            PlayProceduralOnTime();
        }
        else 
        {
            PlayProceduralOnCallback();
        }
    }

    void PlayProceduralOnTime() 
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            AudioManager.Instance.PlayClipOnce(m_SFXList[m_index], transform.position);
            timer = m_soundInterval;
            m_index = m_isRandom ? m_randomIndex : (m_index + 1) % m_SFXList.Length;
        }
    }

    void PlayProceduralOnCallback() 
    {
        AudioManager.Instance.PlayClipOnce(m_SFXList[m_index], transform.position);
        m_index = m_isRandom ? m_randomIndex : (m_index + 1) % m_SFXList.Length;
    }
}
