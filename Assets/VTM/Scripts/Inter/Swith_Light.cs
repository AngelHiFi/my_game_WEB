using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swith_Light : MonoBehaviour, Interactable
{

    // скрипт для вкл выкл объекта с множеством лампочек
    //  - закоментировано управление одной лампой

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip jobAudio;         


    public GameObject m_Light;
    // public Light m_Light;
    public bool isON;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        //m_Light.enabled = isON;
        m_Light.SetActive(isON);

    }

    public string GetDescription()
    {
        if (isON) return "Press [E]";
        return "Press [E]";
    }

    public void Interact()
    {
        isON = !isON;
        //m_Light.enabled = isON;
        m_Light.SetActive(isON);
        playerAudio.PlayOneShot(jobAudio);
    }


}

