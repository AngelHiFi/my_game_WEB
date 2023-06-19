using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press_E : MonoBehaviour
{
	private bool isStay;
	private bool isOUT;              // переключатель открыто-закрыто

	[SerializeField] Animator animator;
	[SerializeField] GameObject pressE;               // сюды кидаем - канвас интерактивной кнопки
	[SerializeField] private AudioSource playerAudio;
	[SerializeField] public AudioClip jobAudio;      // звук рычага

	private void Start()
	{
		isStay = false;
		animator = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && !isStay)
		{
			pressE.gameObject.SetActive(true);  // Видим кнопку
			Debug.Log("Можно нажимать Е");
			//isStay = true;

			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("Игрок нажал Е");
				isStay = true;
				playerAudio.PlayOneShot(jobAudio);
				pressE.gameObject.SetActive(false);     // если нажали Е - кнопка исчезла

				isOUT = !isOUT;

				if(isOUT == true)
                {
					animator.SetTrigger("isRun");
				}

				if(isOUT == false)
                {
					animator.SetTrigger("isRun");
				}
            }

			
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isStay = false;
			pressE.gameObject.SetActive(false);  // при выходе из коллайдера - кнопка исчезла
		}
	}

}
