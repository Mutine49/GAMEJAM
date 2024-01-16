using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class changecam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private GameObject[] tabcam;
    [SerializeField] int currentCamera;
    [SerializeField] private Camera mainCamera; // Assurez-vous d'assigner la cam�ra principale dans l'inspecteur


    // Start is called before the first frame update
    void Start()
    {
        SetActiveCamera(0);
    }

    void SetActiveCamera(int camera)
    {
        currentCamera = camera;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (currentCamera == i)
                cameras[i].enabled = true;
            else
                cameras[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetActiveCamera(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetActiveCamera(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetActiveCamera(2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetActiveCamera(3);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetActiveCamera(4);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetActiveCamera(5);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetActiveCamera(6);
        }
        // Gestion des clics de souris
        if (Input.GetMouseButtonDown(0)) // Bouton gauche de la souris
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                // Parcourez le tableau pour voir si l'objet touch� correspond � l'un des �l�ments
                for (int i = 0; i < tabcam.Length; i++)
            {
                if (hit.collider.gameObject == tabcam[i])
                {
                    SetActiveCamera(i); // Changez la cam�ra en fonction de l'indice de l'objet touch�
                    break; // Arr�tez la boucle une fois que vous avez trouv� l'objet correspondant
                }


            }
        }
    }
}
