using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Select_Audio : MonoBehaviour
{
    public GameObject wind;   // объект с ветром
    public GameObject under;  // объект с подземельем

    private float smena = - 7.0f; // когда наступает смена

    private void Start()
    {
        wind.SetActive(true);
        under.SetActive(false);
    }

    private void Update()
    {
        if(transform.position.y < smena)
        {
            StayUnder();
        }
        else
        {
            StayWind();
        }
    }

    private void StayUnder()
    {
        wind.SetActive(false);
        under.SetActive(true);
    }

    private void StayWind()
    {
        wind.SetActive(true);
        under.SetActive(false);
    }
}
