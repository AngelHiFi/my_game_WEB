using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private float timeDamage = 0.5f; // когда передать урон
	private int minDamage = 7;
	private int maxDamage = 30;
	private int midleDamage;
	
    private void OnTriggerStay(Collider other)
    {
		if (other.CompareTag("Player"))
		{
			timeDamage -= Time.deltaTime;
			
			if(timeDamage <= 0)
			{
				midleDamage = Random.Range(minDamage, maxDamage);
				other.gameObject.GetComponent<Health_2>().ApplyDamage(midleDamage); // это в скрипт здоровья
				other.gameObject.GetComponent<PlayerMove>().Damage(midleDamage);    // это в скрипт движения
				timeDamage = 0.5f;
			}
		}

		if (other.CompareTag("Enemy"))
		{
			timeDamage -= Time.deltaTime;

			if (timeDamage <= 0)
			{
				midleDamage = Random.Range(minDamage, maxDamage);
				other.gameObject.GetComponent<Health_Emeny>().ApplyDamage(midleDamage); // это в скрипт здоровья
				other.gameObject.GetComponent<AI_Controll>().TakeDamage();    // это в скрипт движения
				timeDamage = 0.5f;
			}
		}



	}
}
