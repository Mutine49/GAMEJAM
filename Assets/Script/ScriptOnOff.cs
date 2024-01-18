using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptOnOff : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject buttonporte;
    [SerializeField] private GameObject lumiere;
    [SerializeField] private GameObject buttonlight;
    [SerializeField] private float maxDistance = 5f;

    public bool isDoorClose = false; // Initialement la porte est ouverte
    public bool isLightOff = false; // Initialement la porte est alumée

    void Update()
    {
        if (Mouse.current.leftButton.isPressed) // Détecte le maintien du clic
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Bouton"))
                    isDoorClose = true;
                else
                    isDoorClose = false;
                if (hit.collider.CompareTag("Bouton2"))
                    isLightOff = true; 
                else
                    isLightOff = false;

            }

            else
            {
                isDoorClose = false;
                isLightOff= false;
            }
        }
        else
        {
            isDoorClose = false;
            isLightOff = false;
        }
        door.SetActive(isDoorClose);
        lumiere.SetActive(isLightOff);
    }
}


