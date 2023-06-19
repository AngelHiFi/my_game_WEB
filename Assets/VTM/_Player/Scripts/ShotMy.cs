using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMy : MonoBehaviour
{
    public Rigidbody bullet;          // префаб пули
    public Transform target;          // пустышка с которой летят патроны
    public int bulletSpeed = 3900;    // скорость пули

    public Transform targetMelee;              // пустышка, которая наносит урон Посох
	// private int damage = 10;                   // урон
    private float damageRadius = 1.0f;         // радиус урона

	// урон посохом
	private int minDamage = 7;
	private int maxDamage = 30;
	private int midleDamage;



	// атака Посохом
	public void MeleeAtack()
    {
		midleDamage = Random.Range(minDamage, maxDamage);

		// это метод поиска всех коллайдеров в зоне действия
		Collider[] hitColliders = Physics.OverlapSphere(targetMelee.position, damageRadius);
	   
       foreach(var hitCollider in hitColliders)
	    {
			// чтобы себЯ не пиздить
			if(hitCollider.gameObject.CompareTag("Enemy")) 
			{
				hitCollider.gameObject.SendMessageUpwards("ApplyDamage", midleDamage, SendMessageOptions.DontRequireReceiver);  // урон посоха отправляем в health врага
				hitCollider.gameObject.SendMessageUpwards("TakeDamage", midleDamage, SendMessageOptions.DontRequireReceiver);   // урон посоха в управления врага

			}
		}
	}

    // выстрел Гаубицы (тут пуля всё отправляет)
    public void BombAtack()
		{
			Rigidbody bulletInstace;                                                            // переменная для спауна
			bulletInstace = Instantiate(bullet, target.position, target.rotation) as Rigidbody; // спаун
			bulletInstace.AddForce(target.forward * bulletSpeed);                               // скорость
		}

}
