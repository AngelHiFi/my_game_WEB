using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        RotationShield();
    }

    private void RotationShield()
    {
        transform.Rotate(0, -50.0f * Time.deltaTime, 0); // вращение по оси Y
    }
}
