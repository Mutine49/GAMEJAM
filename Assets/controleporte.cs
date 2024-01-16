using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class controleporte : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject button;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private Camera mainCamera;

    private bool isDoorOpen = false; // Initialement la porte est fermée

    void Update()
    {
        if (Mouse.current.leftButton.isPressed) // Détecte le maintien du clic
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance) )
            {
                if (hit.collider.CompareTag("Bouton"))
                    isDoorOpen = true;
                else
                    isDoorOpen = false;
            }

            else
            {
                isDoorOpen = false;
            }
        }
        else
        {
            isDoorOpen = false;
        }
        door.SetActive(isDoorOpen);
    }
}


