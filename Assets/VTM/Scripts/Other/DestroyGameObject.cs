using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
	// сделал для разрушаемых объектов
	
	private bool isLive;
	private int healthGM = 1;    // здоровье объекта
	//private Animator animator;
	[SerializeField] private AudioSource playerAudio;
	[SerializeField] public AudioClip dead;
	
	public BoxCollider boxCOL;
	
	public GameObject Door;
	public GameObject DoorDead;
	
	void Start()
    {
		isLive = true;
		//animator = GetComponent<Animator>();
		boxCOL = GetComponent<BoxCollider>();
	}

    void Update()
    {
	  if(healthGM < 1)
		{
			if(isLive)
			{
				DeadGM();
			}
		}
    }
	
	// смерть объекта, звук, анимация ..
	private void DeadGM()
	{
		isLive = false;
		Debug.Log("объект погиб");
		//animator.SetTrigger("deadDoor");
		playerAudio.PlayOneShot(dead);
		Destroy(gameObject, 10.0f);
		boxCOL.enabled = !boxCOL.enabled;
		Door.SetActive(false);
		DoorDead.SetActive(true);
    }
	

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
           healthGM -=1;
		}
	}
}
