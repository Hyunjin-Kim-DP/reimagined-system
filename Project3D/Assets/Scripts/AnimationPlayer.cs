using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] Animator m_animator; 
    [SerializeField] List<string> m_stateNames = new List<string>();
    int m_currStateAnimIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayCurrentAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            m_currStateAnimIndex = ++m_currStateAnimIndex % m_stateNames.Count;
            PlayCurrentAnimation();
        }
    }

    void PlayCurrentAnimation() => m_animator.Play(m_stateNames[m_currStateAnimIndex]);
}
