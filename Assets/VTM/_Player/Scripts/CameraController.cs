using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Vector3 offset;

    [Header("Rotate")]
    [SerializeField] public float speedRotate = 10;
    [SerializeField] public float minY;
    [SerializeField] public float maxY;

    [Header("Zoom")]
    [SerializeField] public float speedZoom = 10;
    [SerializeField] public float minZoom = 1.5f;
    [SerializeField] public float maxZoom = 4f;
    [SerializeField] private float distanceFromTarget = 3;

    [SerializeField]  private float inputX;
    [SerializeField]  private float inputY;

    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private Vector3 currentRotation;
    [SerializeField] private Vector3 currentVelocity;

    private void LateUpdate()
    {
        inputX += Input.GetAxis("Mouse X") * speedRotate;
        inputY -= Input.GetAxis("Mouse Y") * speedRotate;
        inputY = Mathf.Clamp(inputY, minY, maxY);
        


        distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel") * speedZoom * Time.deltaTime;
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, minZoom, maxZoom);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(inputY, inputX), ref currentVelocity, smoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = (player.position + offset) - transform.forward * distanceFromTarget;
    }
}
