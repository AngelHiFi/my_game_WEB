using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_2 : MonoBehaviour
{
	// скрипт для игрока 
	public float currentHealth;          // текущее здоровье
    public float maxHealth = 100;        // максимальное здоровье
    private bool isLive;                 // объект жив (чтобы два раза не убивать)
	
	[SerializeField] public PlayerMove playerMove;
	private GameObject Object;  // ищем врага

    public bool hasShield;              // проверка на щит
    public GameObject shieldIndicator;  // сюды индикатор тащим (будем вкл выкл)

    private AudioSource playerAudio;
    public AudioClip addHP;
    public AudioClip shieldUP;
    public AudioClip shieldBack;

    private void Start()
    {
		isLive = true;
        currentHealth = maxHealth;
		Object = GameObject.FindGameObjectWithTag("Enemy");
        playerAudio = GetComponent<AudioSource>();
    }

    // лечилка
    public void AddHP(float addH)
    {
       currentHealth += addH;
       playerAudio.PlayOneShot(addHP);

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
    }

    // столкнулись с щитом
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUP"))
        {
            playerAudio.PlayOneShot(shieldUP);
            hasShield = true;                               // включлился флаг
            Destroy(other.gameObject);                      // префаб щита удалили
            StartCoroutine(PowerupCountdownRoutine());      // обратный отсчет запустили
            shieldIndicator.gameObject.SetActive(true);     // включили индикатор щита
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasShield = false;
        shieldIndicator.gameObject.SetActive(false);
        playerAudio.PlayOneShot(shieldBack);
    }



    // коррекция здоровья, в зависимости от полученного урона
    public void ApplyDamage(float damage)
    {
		if(isLive)
        {
            if(!hasShield)
            {
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    isLive = false;
                    Die();
                }

            }

        }
    }

    public void Die()
	{
        playerMove.Dead();                                // сообщаем в управление игрока
		Object.GetComponent<AI_Snake>().DeadPlayer();    // сообщаем в управление врага/ А если враг мертв, будет ошибка!
	}

}

