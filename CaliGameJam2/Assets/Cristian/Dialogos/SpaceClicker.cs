using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceClicker : MonoBehaviour
{
    private Button myButton;

    void Start()
    {
        // Obtiene automáticamente el Button que está en el mismo GameObject
        myButton = GetComponent<Button>();

        if (myButton == null)
        {
            Debug.LogError("No se encontró un componente Button en este GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (myButton != null)
            {
                myButton.onClick.Invoke();
            }
        }
    }
}
