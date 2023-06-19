using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private AudioSource playerAudio;
	public AudioClip deth;             // звук смерти   
    public float speed = 6.0f;         // скорость врага
    private float zDestroy = - 20.0f;  // позиция уничтожения объектов
    private Rigidbody objectRb;
	private GameManager gameManager;   // 
	private int pointValue = 1;        // если сделать публичной, можно каждому мобу свои балы начислять
	
	public GameObject explosionFx;

   
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
		playerAudio = GetComponent<AudioSource>();
		
		// ищем объект Game Manager (пустышка) с компонентом Скрипт 
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);

        if(transform.position.z < zDestroy)
        {
            Destroy(gameObject);
			Debug.Log("Враг убежал");
			gameManager.GameOver();  // если моб убежал, игра закончилась (передаем)
        }
        
    }
	
		// при столкновение с триггером (пулей)
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Bullet"))
        {
		    Explode();                            // функция партикла
			playerAudio.PlayOneShot(deth, 1.0f);
			Debug.Log("Пуля попала");
            gameManager.UpdateScore(pointValue);  // передаем значение в подсчеты
		    Destroy(other.gameObject);            // уничтожаем Пулю
		    Destroy(gameObject);                  // уничтожаем Объект (врага)
        }
    }
	
	void Explode()
	{
		Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
	}
}
