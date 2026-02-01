using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        audioManager.seleccionAudio(0);
        rb.velocity = transform.right * -speed;
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("paraguas"))
        {
            Destroy(gameObject);
        }
    }
}
