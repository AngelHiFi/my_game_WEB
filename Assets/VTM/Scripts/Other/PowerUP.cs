using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{

    // запускаем таймер и взрываем

    private AudioSource playerAudio;
    public AudioClip bBoom;        
    private float timeLive = 2.2f;
    public GameObject explosionFx;     // партикл 


    public void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        timeLive -= Time.deltaTime;

        if (timeLive <= 0)
        {
            BadaBoom();
            timeLive = 2.2f;
        }

    }

    public void BadaBoom()
    {
        playerAudio.PlayOneShot(bBoom, 1.0f); 
        Explode();                            
        Destroy(gameObject, 0.3f);
    }

    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }


}
