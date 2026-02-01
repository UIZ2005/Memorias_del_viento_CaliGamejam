using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoViento : MonoBehaviour
{
    public float fuerzaViento = 5f;
    private bool enCorriente = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("El objeto necesita un Rigidbody2D.");
        }
    }

    void Update()
    {
        if (enCorriente && Input.GetMouseButton(0)) // Click izquierdo
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaViento);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("viento"))
        {
            enCorriente = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("viento"))
        {
            enCorriente = false;
        }
    }
}
