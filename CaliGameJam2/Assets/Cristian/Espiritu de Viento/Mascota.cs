using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascota : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f; // más alto = más rápido
    public float minDistance = 1.5f;
    public Vector2 offset = new Vector2(-1f, 1f); // detrás y un poco arriba

    public GameObject objetoPrefab; // Prefab a instanciar
    public GameObject imagenLista; // Imagen de habilidad lista
    public GameObject imagenEnfriamiento; // Imagen de habilidad en cooldown

    public Animator animator; // Animator con el trigger "Poder"

    private Vector3 lastPlayerPosition;
    private float direction = 1f;

    private bool habilidadLista = true;
    private float cooldown = 15f; // Tiempo de cooldown
    private float tiempoRestante = 0f;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Seguir al jugador y voltear
        float movement = player.position.x - lastPlayerPosition.x;
        if (movement > 0.01f)
        {
            direction = 1f;
        }
        else if (movement < -0.01f)
        {
            direction = -1f;
        }

        Vector3 dynamicOffset = new Vector3(offset.x * direction, offset.y, 0f);
        Vector3 targetPosition = player.position + dynamicOffset.normalized * minDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * direction;
        transform.localScale = localScale;

        lastPlayerPosition = player.position;

        // Controlar imágenes de UI
        if (habilidadLista)
        {
            imagenLista.SetActive(true);
            imagenEnfriamiento.SetActive(false);
        }
        else
        {
            imagenLista.SetActive(false);
            imagenEnfriamiento.SetActive(true);

            // Actualizar tiempo restante
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0f)
            {
                habilidadLista = true;
            }
        }

        // Lanzar habilidad al presionar E
        if (Input.GetKeyDown(KeyCode.E) && habilidadLista)
        {
            audioManager.seleccionAudio(2);
            StartCoroutine(LanzarHabilidad());
        }
    }

    IEnumerator LanzarHabilidad()
    {
        habilidadLista = false;
        tiempoRestante = cooldown;

        // Activar animación
        if (animator != null)
        {
            animator.SetBool("Poder", true);
        }

        // Esperar 2 segundos (duración de la animación)
        yield return new WaitForSeconds(2f);

        // Desactivar animación
        if (animator != null)
        {
            animator.SetBool("Poder", false);
        }

        // Instanciar el objeto un poco más arriba del player
        Vector3 spawnPosition = player.position + new Vector3(0f, 1f, 0f);
        GameObject instancia = Instantiate(objetoPrefab, spawnPosition, Quaternion.identity);

        // Destruir después de 8 segundos
        Destroy(instancia, 8f);
    }
}