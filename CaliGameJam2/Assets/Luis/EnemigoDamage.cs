using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDamage : MonoBehaviour
{
    public Transform firepoint;
    public GameObject balaPrefab;
    private bool disparar = false;
    private GameObject target;
    public float RangoPLayer = 8;
    public bool dispara = true;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            float distancia = Vector2.Distance(transform.position, target.transform.position);

            if (distancia < RangoPLayer)
            {
                disparar = true;
            }
            else
            {
                disparar = false;
            }
        }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().getDamage();
        }
    }
    public void Disparar()
    {
        if (disparar && dispara)
        {
            Instantiate(balaPrefab, firepoint.position, firepoint.rotation);
        }
    }
}
