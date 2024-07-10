using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ObjectInteraction : MonoBehaviour
{
    // Referencia al Prefab
    public GameObject object3D;

    // Referencia al renderer del Prefab
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = object3D.GetComponent<Renderer>();
    }

    void Update()
    {
        // Verificar si el usuario toca el Prefab
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el Prefab es tocado
                if (hit.transform == object3D.transform)
                {
                    // Capturar el Prefab
                    OnMouseDown();
                }
            }
        }
    }
        void OnMouseDown()
        {
            Debug.Log("El objeto ha sido clickeado!");
        }
}

