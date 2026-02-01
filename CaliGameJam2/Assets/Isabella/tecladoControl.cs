using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class tecladoControl : MonoBehaviour
{
    public Button[] botones; // Asigna los botones en el Inspector
    private int indiceActual = 0;

    void Start()
    {
        // Selecciona el primer botón al iniciar
        if (botones.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(botones[0].gameObject);
        }
    }

    void Update()
    {
        // Mover hacia abajo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceActual = (indiceActual + 1) % botones.Length;
            SeleccionarBoton(indiceActual);
        }

        // Mover hacia arriba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceActual = (indiceActual - 1 + botones.Length) % botones.Length;
            SeleccionarBoton(indiceActual);
        }

        // Seleccionar con Enter o Espacio
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            botones[indiceActual].onClick.Invoke();
        }
    }

    void SeleccionarBoton(int indice)
    {
        EventSystem.current.SetSelectedGameObject(botones[indice].gameObject);
    }
}
