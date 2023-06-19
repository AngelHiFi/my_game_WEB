using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addHealth : MonoBehaviour
{
	private float addH = 100.0f;        // сколько дать здоровья
	private bool yesHP = true;          // чтобы два раза не взять лечилку

	private bool fadeOut, fadeIn;
	private float fadeSpeed = 5.0f;
	private Renderer rend;

	void Start()
	{
		
		rend = GetComponent<Renderer>();
	}

    private void Update()
    {
		RotationHealth();
    }

	private void RotationHealth()
    {
		transform.Rotate(0, -50.0f * Time.deltaTime, 0); // вращение по оси Y
    }

    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (yesHP)
			{
				other.gameObject.GetComponent<Health_2>().AddHP(addH);
			    yesHP = false;
				StartCoroutine(FadeOutObject());
				Destroy(gameObject, 2.0f);
			}
		}
	}

	// исчезновение
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
