using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Bullet : MonoBehaviour
{
    private int minDamage = 7;
    private int maxDamage = 30;
    private int midleDamage;

    private void Start()
    {
        StartCoroutine(LiveBullet());
        midleDamage = Random.Range(minDamage, maxDamage);
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health_Emeny>().ApplyDamage(midleDamage);     // передача урона в класс Health_Enemy, число хп меняем у врага
            Destroy(gameObject);                                             // саму пулю удаляем

            other.GetComponent<AI_Snake>().TakeDamage(); // передаем попадание в скрипт управления Врага
        }
    }

    // время жизни пули
    IEnumerator LiveBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}

