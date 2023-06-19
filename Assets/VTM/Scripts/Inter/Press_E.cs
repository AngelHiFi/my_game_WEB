using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press_E : MonoBehaviour
{
	private bool isStay;
	private bool isOUT;              // ������������� �������-�������

	[SerializeField] Animator animator;
	[SerializeField] GameObject pressE;               // ���� ������ - ������ ������������� ������
	[SerializeField] private AudioSource playerAudio;
	[SerializeField] public AudioClip jobAudio;      // ���� ������

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
			pressE.gameObject.SetActive(true);  // ����� ������
			Debug.Log("����� �������� �");
			//isStay = true;

			if (Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("����� ����� �");
				isStay = true;
				playerAudio.PlayOneShot(jobAudio);
				pressE.gameObject.SetActive(false);     // ���� ������ � - ������ �������

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
			pressE.gameObject.SetActive(false);  // ��� ������ �� ���������� - ������ �������
		}
	}

}
