using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Emeny : MonoBehaviour
{
    // ��� �����
    public float currentHealth;          // ������� ��������
    public float maxHealth = 100;        // ������������ ��������
    private bool isLive;                 // ������ ��� (����� ��� ���� �� �������)

    [SerializeField] public AI_Snake ai_snake;

    private void Start()
    {
        isLive = true;
        currentHealth = maxHealth;
    }

    // �������
    public void AddHP(float addH)
    {
        currentHealth += addH;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // ��������� ��������, � ����������� �� ����������� �����
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
