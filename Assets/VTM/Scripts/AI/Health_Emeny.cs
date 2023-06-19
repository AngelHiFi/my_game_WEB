using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Emeny : MonoBehaviour
{
    // для мобов
    public float currentHealth;          // текущее здоровье
    public float maxHealth = 100;        // максимальное здоровье
    private bool isLive;                 // объект жив (чтобы два раза не убивать)

    [SerializeField] public AI_Snake ai_snake;

    private void Start()
    {
        isLive = true;
        currentHealth = maxHealth;
    }

    // лечилка
    public void AddHP(float addH)
    {
        currentHealth += addH;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // коррекция здоровья, в зависимости от полученного урона
    public void ApplyDamage(float damage)
    {
        if (isLive)
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

    public void Die()
    {
        ai_snake.Dead();
    }
}
