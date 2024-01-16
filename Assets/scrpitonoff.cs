using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class scriptonoff : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject buttonporte;
    [SerializeField] private GameObject lumiere;
    [SerializeField] private GameObject buttonlight;
    [SerializeField] private float maxDistance = 5f;

    private bool isDoorOpen = false; // Initialement la porte est fermée
    private bool isLightOn = false; // Initialement la porte est fermée

    void Update()
    {
        if (Mouse.current.leftButton.isPressed) // Détecte le maintien du clic
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Bouton"))
                    isDoorOpen = true;
                else
                    isDoorOpen = false;
                if (hit.collider.CompareTag("Bouton2"))
                    isLightOn = true; 
                else
                    isLightOn = false;

            }

            else
            {
                isDoorOpen = false;
                isLightOn= false;
            }
        }
        else
        {
            isDoorOpen = false;
            isLightOn = false;
        }
        door.SetActive(isDoorOpen);
        lumiere.SetActive(isLightOn);
    }
}


