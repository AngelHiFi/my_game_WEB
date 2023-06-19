using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private bool isItem = true;          // чтобы два раза не посчитался Один и тот же предмет
	private GameManager gameManager;     // сюды передаем собранные 
	private int pointValue = 1;          // что передаем за каждый собранный итем 

	private AudioSource playerAudio;
	public AudioClip item;              // звук при взятии

	private MeshRenderer rend;
	private float fadeSpeed = 5.0f;

	private void Start()
    {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		playerAudio = GetComponent<AudioSource>();
		rend = GetComponent<MeshRenderer>();

	}



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (isItem)
			{
				
				playerAudio.PlayOneShot(item, 1.0f);
				gameManager.UpdateScore(pointValue);  // передаем значение в подсчеты
				isItem = false;
				StartCoroutine(FadeOutObject());
				Destroy(gameObject, 0.5f);
			}
		}
	}

	public IEnumerator FadeOutObject()
	{
		while (this.rend.material.color.a > 0)
		{
			Color objectColor = this.rend.material.color;
			float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			this.rend.material.color = objectColor;
			yield return null;
		}
	}


}
