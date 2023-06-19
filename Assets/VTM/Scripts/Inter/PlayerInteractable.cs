using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractable : MonoBehaviour
{
    public Camera mainCamera;
    //public GameObject mainCamera;

    public float intDistance = 30f;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;


 
    void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if(Physics.Raycast(ray, out hit, intDistance))
        {
            Interactable interactible = hit.collider.GetComponent<Interactable>();

            if(interactible !=null)
            {
                hitSomething = true;
                interactionText.text = interactible.GetDescription();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactible.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}
