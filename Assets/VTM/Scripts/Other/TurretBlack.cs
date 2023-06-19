using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBlack: MonoBehaviour 

{
	private AudioSource playerAudio;
	public AudioClip shot;        // звук выстрела

	Transform player;             // игрок
	float dist;                   // расстояние до игрока
	public float howClose;        //  расстояние, агр турели 
	public Transform head;        // башка башни
	public GameObject bullet;     // префаб пули
	public Transform bulletPoint; // пустышка, с которой летят пули
	public int speedBullet;       // скорость пули
	
	public float fireRate = 2;    // скорость стрельбы
	public float nextFire;        // когда следующий выстрел
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerAudio = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		dist = Vector3.Distance(player.position, transform.position);
		
		if(dist <= howClose)           // проверка, зашел ли игрок в зону агра
		{
			head.LookAt(player);       // башка посмотри на игрока
			
			if(Time.time >= nextFire)  // проверка, пора ли стрелять
			{
				nextFire = Time.time + 1f / fireRate;
				Shoot();
			}
		}
	}
	
	void Shoot()
	{
		playerAudio.PlayOneShot(shot, 1.0f);     // звук выстрела

        // создать пулю в точке спауна
		GameObject clone = Instantiate(bullet, bulletPoint.position, head.rotation); 
		
		// добавить движение созданному объекту
		clone.GetComponent<Rigidbody>().AddForce(head.forward * speedBullet);
		
		// уничтожение клона префаба (пулю уничтожаем)
		Destroy(clone, 3);
	}
}
