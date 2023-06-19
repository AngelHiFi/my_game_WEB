using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip tpSound;        
	public Transform teleportPoint;     // точка выхода с тп
    private CharacterController myCC;
    private bool isTP = true;

    public void Start()
    {
       myCC = GameObject.Find("Player").GetComponent<CharacterController>();
       playerAudio = GetComponent<AudioSource>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Player")
        {
            if(isTP)
            {
                playerAudio.PlayOneShot(tpSound, 1.0f);
                myCC.enabled = false;
                other.transform.position = teleportPoint.transform.position;
                myCC.enabled = true;
                isTP = false;
				StartCoroutine(TPtime());
			}
        }
    }
	
	IEnumerator TPtime()
	{
		yield return new WaitForSeconds(2);
		isTP = true;
		Debug.Log("Телепорт готов!");
	}
}


