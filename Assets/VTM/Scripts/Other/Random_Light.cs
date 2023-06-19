using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Light : MonoBehaviour
{
    public AudioSource electro;

    public Light _light;
    
    private float start_intensity;
    private float min_int = 0.3f;
    private float max_int = 1.5f;
    private float noise_speed = 0.15f;
    private bool flicker_ON;
    private bool random_time;
    private float random_min = 5f;
    private float random_max = 20f;
    private float random_time_value;
    private float start_time;

    IEnumerator Start()
    {
        
        _light = GetComponent<Light>();
        start_intensity = _light.intensity;
        yield return new WaitForSeconds(start_time);

        if (flicker_ON) electro.Play();

        while(random_time)
        {
            random_time_value = Random.Range(random_min, random_max);
            yield return new WaitForSeconds(start_time);
            if(flicker_ON)
            {
                _light.intensity = start_intensity;
                electro.Play();
                flicker_ON = false;
            }

            else
            {
                electro.Play();
                flicker_ON = true;
            }
        }
    }

    private void Update()
    {
        if (flicker_ON) _light.intensity = Mathf.Lerp(min_int, max_int, Mathf.PerlinNoise(10, Time.time / noise_speed));
        
    }


}
