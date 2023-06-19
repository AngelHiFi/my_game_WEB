using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
	// урон от моба
    private int minDamage = 7;
	private int maxDamage = 30;
	private int midleDamage;
	
	 private float damageRadius = 10.0f;         // радиус урона
	 public Transform targetMelee;              // пустышка, от которой мы делаем радиус

	

	
	// урон для игрока
	public void EnemyMageAttack()
	{
		midleDamage = Random.Range(minDamage, maxDamage);

		// это метод поиска всех коллайдеров в зоне действия
		Collider[] hitColliders = Physics.OverlapSphere(targetMelee.position, damageRadius);

		foreach (var hitCollider in hitColliders)
		{
			
			if (hitCollider.gameObject.CompareTag("Player"))    // чтобы враг себе не навредил
			{
				hitCollider.gameObject.SendMessageUpwards("ApplyDamage", midleDamage, SendMessageOptions.DontRequireReceiver); // урон магии в здоровье игрока
				//hitCollider.gameObject.SendMessageUpwards("Damage", midleDamage, SendMessageOptions.DontRequireReceiver);    
				// урон магии в управление игрока, почему то, не дает норм анимацию смерти игрока (?)
			}
		}
    }
}
