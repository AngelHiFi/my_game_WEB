using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{

    private AudioSource playerAudio;
    public AudioClip platformSound;              
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    private void Platforms()
    {
        playerAudio.PlayOneShot(platformSound);
    }
}
