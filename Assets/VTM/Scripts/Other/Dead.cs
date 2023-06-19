using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
	private bool isLive = true;
	


	
    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Player"))
		{
			if(isLive)
			{
				other.gameObject.GetComponent<PlayerMove>().Dead();
				isLive = false;
			}
		}

		if (other.CompareTag("Enemy"))
        {
			if(isLive)
            {
				other.gameObject.GetComponent<AI_Controll>().Dead();
				isLive = false;
				Debug.Log("враг в зоне смерти");
			}
        }
		
      
	}

}
