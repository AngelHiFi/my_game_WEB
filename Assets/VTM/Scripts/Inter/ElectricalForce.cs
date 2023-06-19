using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalForce : MonoBehaviour, Interactable
{

    public Animator anim;
    public bool isOpen;
    public GameObject door;  // сюда кидаем силовой щит Plane

    

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip jobAudio;         // звук рычага

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        door.SetActive(true);


        if (isOpen)
            anim.SetBool("isOpen", true);
        
    }

    public string GetDescription()
    {
        if (isOpen) return "Press [E]";
        return "Press [E]";
    }

    public void Interact()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            anim.SetBool("isOpen", true);
            playerAudio.PlayOneShot(jobAudio);
            StartCoroutine(OpenDoor());
            // door.SetActive(false);
        }
           
        else
        {
            anim.SetBool("isOpen", false);
            playerAudio.PlayOneShot(jobAudio);
            StartCoroutine(CloseDoor());
            // door.SetActive(true);
        }
           
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(2);
        door.SetActive(false);
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2);
        door.SetActive(true);
    }

}
