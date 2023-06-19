using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSecret : MonoBehaviour
{
	private AudioSource playerAudio;
	public AudioClip secret; 
    
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }


	
			// для коллизий, то же надо, чтобы один был Rigidbody
		private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("PowerUP"))
		{
			playerAudio.PlayOneShot(secret, 1.0f);
			Destroy(collision.gameObject);  // Вот так пишется в коллизиях!
			Debug.Log("Лечилка");
		}
	}
}
