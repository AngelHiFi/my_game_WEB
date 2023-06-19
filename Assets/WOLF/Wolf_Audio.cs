using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_Audio : MonoBehaviour
{

    private AudioSource source;
    public AudioClip attack;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void WOLF_Attack()
    {
        source.PlayOneShot(attack);
    }


}
