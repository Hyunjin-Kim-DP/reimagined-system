using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorCanvas : MonoBehaviour
{
    [SerializeField] PlayerController m_player;
    [SerializeField] Text m_speedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_speedText.text = m_player.GetComponent<Rigidbody>().velocity.magnitude.ToString();
    }
}
