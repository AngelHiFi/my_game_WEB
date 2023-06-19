using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_Emeny : MonoBehaviour
{
    public Health_Emeny Health;              // ������ �����
    public Image imageBar;             // ������� �����
    public Text textBar;               // ����� 
    public Transform pivotBar;         // ���� ��� �����
    public bool hideBar;               // ������� ��� ������� ����

    private void Update()
    {
        // �������� �������� ����� ��������
        imageBar.fillAmount = Health.currentHealth / Health.maxHealth; // 100/100 = 1, � 1 ��� ������ �����.

        // ��������� ������ �������� ����� � ������/������
        // �� ������ ���� �������� ��� MainCamera
        pivotBar.LookAt(Camera.main.transform.position);

        // ������ ������ �������� (� ����������), ���� ����
        if (hideBar)
            pivotBar.gameObject.SetActive(imageBar.fillAmount != 1);

        // ���������� ����� � ������
        textBar.text = $"{ Health.currentHealth} / { Health.maxHealth }";

    }


}
