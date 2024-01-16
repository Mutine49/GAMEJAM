using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class controlelumière : MonoBehaviour
{
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject button;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private Camera mainCamera;

    private bool isLightOn = false; // Initialement la porte est fermée

    void Update()
    {
        if (Mouse.current.leftButton.isPressed) // Détecte le maintien du clic
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Bouton2"))
                    isLightOn = true;
                else
                    isLightOn = false;
            }

            else
            {
                isLightOn = false;
            }
        }
        else
        {
            isLightOn = false;
        }
        light.SetActive(isLightOn);
    }
}