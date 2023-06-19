using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_2 : MonoBehaviour

{
    public Health_2 Health;              // скрипт жизни
    public Image imageBar;             // полоска жизни
    public Text textBar;               // числа 
    public Transform pivotBar;         // весь бар жизни
    public bool hideBar;               // булевая для скрытия бара

    private void Update()
    {
        // обновить значение шкалы здоровья
        imageBar.fillAmount = Health.currentHealth / Health.maxHealth; // 100/100 = 1, а 1 это полная шкала.

        // повернуть полосу здоровья лицом к игроку/камере
        // на камере надо включить тэг MainCamera
        pivotBar.LookAt(Camera.main.transform.position);

        // скрыть полосу здоровья (в инспекторе), если надо
        if (hideBar)
            pivotBar.gameObject.SetActive(imageBar.fillAmount != 1);

        // показывает жизни в числах
        textBar.text = $"{ Health.currentHealth} / { Health.maxHealth }";

    }

}
