using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBlack_bullet : MonoBehaviour
{
	[SerializeField] private float damage = 10;


    void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Health_2>().ApplyDamage(damage);
            Destroy(gameObject);
		}
	}


}
