using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] GameObject m_audioObjectPrefab;

    [Space]
    [Range(0f, 2f), SerializeField] float m_globalVolume = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClipOnce(AudioClip clip, Vector3 position, float volume = 1, float sptial = 0) 
    {
        var sfx = Instantiate(m_audioObjectPrefab).GetComponent<AudioSource>();

        sfx.transform.position = position;
        sfx.clip = clip;
        sfx.spatialBlend = sptial;
        sfx.volume = volume * m_globalVolume;
        sfx.Play();

        Destroy(sfx.gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale)); // following time scale
    }
}
