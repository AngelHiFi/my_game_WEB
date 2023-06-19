using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Материал должен быть с прозрачностью!

public class FadeObject : MonoBehaviour
{
	private bool fadeOut, fadeIn;
	public float fadeSpeed = 5.0f;
	private Renderer rend;
	
	void Start()
	{
		rend = GetComponent<Renderer>();
	}
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.H))
		{
			StartCoroutine(FadeOutObject());   // исчез
		}
		
		if(Input.GetKeyDown(KeyCode.J))
		{
			StartCoroutine(FadeInObject());   // появл
		}
	}
	
	public IEnumerator FadeOutObject()
	{
		while(this.rend.material.color.a > 0)
		{
			Color objectColor = this.rend.material.color;
			float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
			
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			this.rend.material.color = objectColor;
			yield return null;
		}
	}
	
	public IEnumerator FadeInObject()
	{
		while(this.rend.material.color.a  < 1)
		{
			Color objectColor = this.rend.material.color;
			float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
			
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
			this.rend.material.color = objectColor;
			yield return null;
		}
	}
}
